using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public string[] dialogueLines;

    private bool isPlayerInRange = false;
    [SerializeField] private GameObject interactSymbol;


    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            isPlayerInRange = true;
            if (interactSymbol != null)
                interactSymbol.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            isPlayerInRange = false;

            if (interactSymbol != null)
                interactSymbol.SetActive(false);
        }
    }

    void StartDialogue()
    {
        dialogueManager.StartDialogue(dialogueLines);
    }
}
