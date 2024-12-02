using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public Button continueButton;



    private Queue<string> dialogueQueue;

    void Start()
    {
        dialogueQueue = new Queue<string>();
        dialogueBox.SetActive(false);

        continueButton.onClick.AddListener(DisplayNextDialogue);
    }


    public void StartDialogue(string[] lines)
    {
        dialogueQueue.Clear();

        foreach (string line in lines)
        {
            dialogueQueue.Enqueue(line);
        }

        dialogueBox.SetActive(true);
        GameManager.instance.PauseGame(true);
        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string line = dialogueQueue.Dequeue();
        dialogueText.text = line;
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        GameManager.instance.PauseGame(false);
    }
}
