using UnityEngine;
using TMPro;
using System.Collections;

public class GhostDialogueNew : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject dialogueBorder;
    public GameObject pressETextObject;
    public TMP_Text dialogueText;

    public float typingSpeed = 0.04f;

    private bool playerNear = false;
    private bool dialogueOpen = false;
    private bool isTyping = false;

    private int dialogueIndex = 0;
    private Coroutine typingCoroutine;

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
        dialogueText.text = "";
    }

    void Update()
    {
        if (playerNear && !dialogueOpen && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }

        if (dialogueOpen && Input.GetKeyDown(KeyCode.Return))
        {
            if (isTyping)
            {
                ShowFullLine();
            }
            else
            {
                NextDialogueLine();
            }
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

        StartTypingLine();
    }

    void StartTypingLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeLine(dialogueLines[dialogueIndex]));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in line)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void ShowFullLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        dialogueText.text = dialogueLines[dialogueIndex];
        isTyping = false;
    }

    void NextDialogueLine()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogueLines.Length)
        {
            StartTypingLine();
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueOpen = false;
        isTyping = false;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        dialogueText.text = "";

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
            isTyping = false;

            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            dialogueText.text = "";

            pressETextObject.SetActive(false);
            dialoguePanel.SetActive(false);
            dialogueBorder.SetActive(false);
        }
    }
}