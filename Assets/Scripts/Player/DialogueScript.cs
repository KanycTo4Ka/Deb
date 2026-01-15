using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class DialogueScript : MonoBehaviour, IInteractable
{
    [TextArea]
    [SerializeField] string description;
    [TextArea]
    [SerializeField] public List<string> messages;

    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text messageText;
    [SerializeField] TMP_Text priceText;

    [SerializeField] Spawner spawner;

    [SerializeField] Transform Player;

    [SerializeField] ScriptLocker scriptLocker;

    int soulPrice = 0;

    public string getDescription()
    {
        return description;
    }

    public void interact(PlayerScript player)
    {
        scriptLocker.lockScripts();
        dialoguePanel.SetActive(true);
        priceText.text = "Требуется душ: " + soulPrice.ToString();
        messageText.text = messages[Random.Range(0, messages.Count)];
    }

    public void onDialogueEnd()
    {
        scriptLocker.unlockScripts();
        dialoguePanel.SetActive(false);
        Player.GetComponent<PlayerRotation>().enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialoguePanel.SetActive(false);
            scriptLocker.unlockScripts();
        }
    }

    public void generateMaze()
    {
        if (Player.GetComponent<PlayerScript>().getSoul() >= soulPrice)
        {
            Player.GetComponent<PlayerScript>().removeSoul(soulPrice);
            Player.GetComponent<Health>().changeHealth(200);
            soulPrice++;
            spawner.GenerateMaze();
            dialoguePanel.SetActive(false);
            scriptLocker.unlockScripts();
        }
        else
        {
            Player.GetComponent<Health>().death();
            dialoguePanel.SetActive(false);
        }
    }
}
