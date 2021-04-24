using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = FindObjectOfType<SFXManager>();
            }
            if (!m_instance)
            {
                SFXManager prefab = Resources.Load<SFXManager>("SFXManager");
                m_instance = UnityEngine.Object.Instantiate(prefab);
            }
            return m_instance;
        }
    }
    private static SFXManager m_instance = null;

    [SerializeField] AudioSource sfxTemplate = null;

    void Awake()
    {
        if (instance != null && m_instance != this)
        {
            Destroy(gameObject);
            return;
        }
        if (m_instance == this)
            DontDestroyOnLoad(gameObject);
    }

    public static void PlaySFX(AudioClip clip)
    {
        if (!instance)
            return;

        AudioSource sfx = UnityEngine.Object.Instantiate(instance.sfxTemplate);
        sfx.clip = clip;
        Destroy(sfx.gameObject, 2);
        DontDestroyOnLoad(sfx);
        sfx.Play();
        sfx.transform.position = Camera.main.transform.position;
    }
}