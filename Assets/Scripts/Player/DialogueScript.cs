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

    public string getDescription()
    {
        return description;
    }

    public void interact(PlayerScript player)
    {
        dialoguePanel.SetActive(true);
        messageText.text = messages[Random.Range(0, messages.Count)];
    }

    public void onDialogueEnd(PlayerScript player)
    {
        dialoguePanel.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            dialoguePanel.SetActive(false);
    }

    public void generateMaze()
    {
        if (true)
            spawner.GenerateMaze();
    }
}
