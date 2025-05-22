using UnityEngine;
using UnityEngine.UI;

public class TriggerText : MonoBehaviour
{
    public GameObject displayText; // Для обычного UI Text

    void Start()
    {
        if (displayText != null)
            displayText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (displayText != null)
            displayText.gameObject.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (displayText != null)
            displayText.gameObject.SetActive(false);
    }
}