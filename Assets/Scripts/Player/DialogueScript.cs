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

    [SerializeField] Spawner spawner;

    [SerializeField] Transform Player;

    public string getDescription()
    {
        return description;
    }

    public void interact(PlayerScript player)
    {
        Player.GetComponent<PlayerRotation>().enabled = false;
        dialoguePanel.SetActive(true);
        messageText.text = messages[Random.Range(0, messages.Count)];
    }

    public void onDialogueEnd()
    {
        dialoguePanel.SetActive(false);
        Player.GetComponent<PlayerRotation>().enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialoguePanel.SetActive(false);
            Player.GetComponent<PlayerRotation>().enabled = true;
        }
    }

    public void generateMaze()
    {
        if (true)
        {
            spawner.GenerateMaze();
            dialoguePanel.SetActive(false);
            Player.GetComponent<PlayerRotation>().enabled = true;
        }
    }
}
