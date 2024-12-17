using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinSceneText : MonoBehaviour
{
    public Text dialogueText; // Reference to the Text component
    public GameObject dialogueBackground; // Reference to the background GameObject

    private Queue<string> dialogueMessages; // Queue to hold the win scene messages
    private bool isDialogueActive = true; // Flag to check if the dialogue is active

    void Start()
    {
        // Initialize the queue
        dialogueMessages = new Queue<string>();

        // Ensure the dialogue UI starts hidden
        dialogueBackground.SetActive(false);
        // Debug.Log("Win scene dialogue background hidden at start.");

        // Add win scene messages
        dialogueMessages.Enqueue("I did it... The witch is finally defeated!\n(R to continue)");
        dialogueMessages.Enqueue("It wasn't easy, but my perseverance paid off.\n(R to continue)");
        dialogueMessages.Enqueue("This place is safe again, thanks to me!!\n(R to continue)");
        dialogueMessages.Enqueue("It's time to leave this chapter behind and look forward to new adventures...\n(R to finish)");

        // Start the win scene dialogue
        DisplayNextMessage();
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.R))
        {
            // Debug.Log("R key pressed in win scene.");
            DisplayNextMessage();
        }
    }

    void DisplayNextMessage()
    {
        if (dialogueMessages.Count == 0)
        {
            EndDialogue();
            return;
        }

        // Activate the dialogue UI
        dialogueBackground.SetActive(true);
        // Debug.Log("Win scene dialogue background activated.");

        // Get the next message from the queue
        string message = dialogueMessages.Dequeue();
        dialogueText.text = message; // Update the text with the current message
        // Debug.Log($"Displayed message: {message}");
    }

    void EndDialogue()
    {
        // Deactivate the dialogue UI
        dialogueBackground.SetActive(false);
        // Debug.Log("Win scene dialogue background hidden. Dialogue ended.");

        dialogueText.text = ""; // Clear the dialogue text
        isDialogueActive = false; // Mark the dialogue as finished
    }
}

