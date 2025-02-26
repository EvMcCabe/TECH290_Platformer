using UnityEngine;
using TMPro; // If using TextMeshPro

public class SignInteraction : MonoBehaviour
{
    public GameObject textBoxUI; // Assign your UI Panel
    public TMP_Text textComponent; // Assign your TextMeshPro text (or use Text if not using TMP)
    public string signText = "Welcome to this area!"; // Set the text for this sign
    
    private bool isPlayerNearby = false;
    private bool isTextActive = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (!isTextActive)
            {
                ShowText();
            }
            else
            {
                HideText();
            }
        }
    }

    private void ShowText()
    {
        textBoxUI.SetActive(true);
        textComponent.text = signText;
        isTextActive = true;
    }

    private void HideText()
    {
        textBoxUI.SetActive(false);
        isTextActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            HideText(); // Hide text when the player leaves
        }
    }
}
