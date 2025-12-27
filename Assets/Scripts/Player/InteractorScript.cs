using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InteractorScript : MonoBehaviour
{
    [SerializeField] PlayerScript player;
    [SerializeField] TMP_Text messageText;
    [SerializeField] GameObject interactorPanel;

    public IInteractable interactable = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactorPanel.SetActive(true);
                messageText.text = interactable.getDescription();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            interactorPanel.SetActive(false);
            interactable = null;
            messageText.text = "";
        }
    }
}
