using UnityEngine;
using TMPro;

public class GhostDialogueNew : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject dialogueBorder;
    public GameObject pressETextObject;
    public TMP_Text dialogueText;

    private bool playerNear = false;
    private bool dialogueOpen = false;
    private int dialogueIndex = 0;

    private string[] dialogueLines =
    {
        "...",
        "You are no longer in your classroom.",
        "This place exists between dreams and reality.",
        "If you want to return home, you must choose one of these doors.",
        "But be careful...",
        "Only one door will lead you forward.",
        "The others may take you deeper into this strange world.",
        "Look closely. Think carefully. Then make your choice."
    };

    void Start()
    {
        dialoguePanel.SetActive(false);
        dialogueBorder.SetActive(false);
        pressETextObject.SetActive(false);
    }

    void Update()
    {
        if (playerNear && !dialogueOpen && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }

        if (dialogueOpen && Input.GetKeyDown(KeyCode.Return))
        {
            NextDialogueLine();
        }

        if (dialogueOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            EndDialogue();
        }
    }

    void StartDialogue()
    {
        dialogueOpen = true;
        dialogueIndex = 0;

        pressETextObject.SetActive(false);
        dialoguePanel.SetActive(true);
        dialogueBorder.SetActive(true);

        dialogueText.text = dialogueLines[dialogueIndex];
    }

    void NextDialogueLine()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[dialogueIndex];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueOpen = false;

        dialoguePanel.SetActive(false);
        dialogueBorder.SetActive(false);

        if (playerNear)
        {
            pressETextObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;

            if (!dialogueOpen)
            {
                pressETextObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            dialogueOpen = false;
            dialogueIndex = 0;

            pressETextObject.SetActive(false);
            dialoguePanel.SetActive(false);
            dialogueBorder.SetActive(false);
        }
    }
}