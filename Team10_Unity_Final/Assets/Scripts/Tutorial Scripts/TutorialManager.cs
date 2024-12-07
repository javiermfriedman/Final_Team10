using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Use this if you're using the standard UI Text
using UnityEngine.SceneManagement; // Required to get the active scene

public class TutorialManager : MonoBehaviour
{
    public Text tutorialText; // Reference to the Text component

    private Queue<string> tutorialMessages; // Queue to hold the tutorial messages
    private bool isTutorialActive = true; // Flag to check if the tutorial is active

    void Start()
    {
        // Initialize the queue
        tutorialMessages = new Queue<string>();

        // Determine the current level (scene) and load the corresponding messages
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Tutorial")
        {
            tutorialMessages.Enqueue("Where am I? What is this place? I must find out!\n(R to continue)");
            tutorialMessages.Enqueue("WASD or arrow keys to move...\n(R to continue)");
            tutorialMessages.Enqueue("Looks like I'm in a graveyard!\n(R to continue)");
            tutorialMessages.Enqueue("I need to continue exploring this space to see what is in store for me...\n(R to finish)");
        }

        // Start the tutorial
        DisplayNextMessage();
    }

    void Update()
    {
        // Check for space bar input to display the next message
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

        // Get the next message from the queue
        string message = tutorialMessages.Dequeue();
        tutorialText.text = message; // Update the text with the current message
    }

    void EndTutorial()
    {
        tutorialText.text = ""; // Clear the tutorial text
        isTutorialActive = false; // Mark the tutorial as finished
        // Add any additional logic for when the tutorial ends
    }
}
