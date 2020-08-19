using RTLTMPro;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecisionButton : MonoBehaviour
{
    public void SetUp(string words)
    {
        if (GetComponentInChildren<RTLTextMeshPro>())
            GetComponentInChildren<RTLTextMeshPro>().text = words;
        else if (GetComponentInChildren<TextMeshProUGUI>())
            GetComponentInChildren<TextMeshProUGUI>().text = words;
        else if (GetComponent<Text>())
            GetComponentInChildren<Text>().text = words;
        else
            Debug.LogWarning("Template Must Have A Text Component");
    }
}