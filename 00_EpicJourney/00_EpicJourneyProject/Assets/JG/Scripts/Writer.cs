using System.Collections;
using System.Linq;
using RTLTMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Writer : MonoBehaviour
{
    public static Writer instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<Writer>();
            if (m_instance == null)
            {
                GameObject g = Resources.Load<GameObject>("Writer");
                if (g)
                    m_instance = Instantiate(g).GetComponent<Writer>();
            }
            if(es == null)
                es = FindObjectOfType<EventSystem>();
            if (es == null)
            {
                GameObject e = Resources.Load<GameObject>("EventSystem");
                es = Instantiate(e).GetComponent<EventSystem>();
            }
            return m_instance;
        }
    }
    static Writer m_instance = null;
    static EventSystem es;
    public enum OnFinish { Quit,Restart }

    [Header("Properties")]
    [SerializeField] Button quitButton = null;
    [SerializeField] Transform decisionsHead = null;
    [SerializeField] GameObject decisionTemplate = null;
    [SerializeField] RTLTextMeshPro text = null;
    [SerializeField] RTLTextMeshPro titleText = null;
    [Header("Read Only")]
    [SerializeField] TextAsset activeStory  = null;
    [SerializeField] string lastWrittenStory = "";
    void Awake()
    {
        quitButton.onClick.AddListener(CloseGame);
        m_instance = this;
    }
    private void CloseGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

#if !UNITY_EDITOR
        Application.Quit();
#endif
    }

    public static void CreateWriter(string ofName)
    {
        GameObject gw = Resources.Load<GameObject>(ofName);
        if(!gw)
        {
            Debug.LogWarning("there was no writer of " + ofName + " Make Sure it exists in the Resources Directory, for now, Creating Default Writer " + instance.name);
            return;
        }
        Writer w = gw.GetComponent<Writer>();
        m_instance = Instantiate(w);

        if (es == null)
            es = FindObjectOfType<EventSystem>();
        if (es == null)
        {
            GameObject e = Resources.Load<GameObject>("EventSystem");
            es = Instantiate(e).GetComponent<EventSystem>();
        }
    }
    public static void SetTitle(string t)
    {
        TextAsset s = Resources.LoadAll<TextAsset>("").ToList().
                                            Find(testFile => testFile != null && testFile.name == t);
        if (s == null)
        {
            Debug.LogWarning("Could Not Locate Title File of name " + t + " make sure it exists in Resources directory");
            return;
        }
        instance.titleText.text = s.text;
    }
    public static void Write(string storyName)
    {
        if (instance.lastWrittenStory == storyName)
            return;

        instance.lastWrittenStory = storyName;

        TextAsset s = Resources.LoadAll<TextAsset>("").ToList().
                                            Find(testStory => testStory != null && testStory.name == storyName);
        if (s == null)
        {
            Debug.LogWarning("Could Not Locate Story Of Name " + storyName + " make sure it exists in Resources directory");
            return;
        }
        Write(s);
    }
    public static void ClearDecisions()
    {
        int childCount = instance.decisionsHead.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(instance.decisionsHead.GetChild(i).gameObject);
        }

        LayoutRebuilder.MarkLayoutForRebuild(instance.GetComponent<RectTransform>());
    }
    public static void AddDecision(string decision)
    {
        var group = instance.decisionsHead.GetComponentsInChildren<RTLTextMeshPro>();
        foreach (var text in group)
        {
            if (text.text == decision)
                return;
        }

        GameObject dec = Instantiate(instance.decisionTemplate, instance.decisionsHead);
        dec.GetComponentInChildren<DecisionButton>().SetUp(decision);
        LayoutRebuilder.MarkLayoutForRebuild(instance.GetComponent<RectTransform>());
    }

    private static void Write(TextAsset storyPage)
    {
        if (instance == null)
        {
            Debug.LogWarning("There Is No Writer In Scene, Please Create or add one");
            return;
        }

        instance.activeStory = storyPage;
        instance.text.text = storyPage.text;
        ClearDecisions();
        LayoutRebuilder.MarkLayoutForRebuild(instance.GetComponent<RectTransform>());
    }
}