using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public Text tutorialText; // Reference to the Text component
    public GameObject tutorialBackground; // Reference to the background GameObject

    private Queue<string> tutorialMessages; // Queue to hold the tutorial messages
    private bool isTutorialActive = true; // Flag to check if the tutorial is active

    void Start()
    {
        // Initialize the queue
        tutorialMessages = new Queue<string>();

        // Ensure the tutorial UI starts hidden
        tutorialBackground.SetActive(false);

        // Determine the current level (scene) and load the corresponding messages
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Tutorial")
        {
            tutorialMessages.Enqueue("Where am I? What is this place? I must find out!\n(R to continue)");
            tutorialMessages.Enqueue("WASD or arrow keys to move...\n(R to continue)");
            tutorialMessages.Enqueue("Looks like I'm in a graveyard! I wonder where this path leads... \n(R to continue)");
            tutorialMessages.Enqueue("I need to continue exploring this space to see what is in store for me...\n(R to continue)");
            tutorialMessages.Enqueue("It seems like I should follow this compass in the bottom right corner...\n(R to finish)");
        }

        // Start the tutorial
        DisplayNextMessage();
    }

    void Update()
    {
        if (isTutorialActive && Input.GetKeyDown(KeyCode.R))
        {
            DisplayNextMessage();
        }
    }

    void DisplayNextMessage()
    {
        if (tutorialMessages.Count == 0)
        {
            EndTutorial();
            return;
        }

        // Activate the tutorial UI
        tutorialBackground.SetActive(true);

        // Get the next message from the queue
        string message = tutorialMessages.Dequeue();
        tutorialText.text = message; // Update the text with the current message
    }

    void EndTutorial()
    {
        // Deactivate the tutorial UI
        tutorialBackground.SetActive(false);

        tutorialText.text = ""; // Clear the tutorial text
        isTutorialActive = false; // Mark the tutorial as finished
    }
}
