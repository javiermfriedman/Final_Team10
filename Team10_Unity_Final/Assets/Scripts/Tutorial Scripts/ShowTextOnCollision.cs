using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Use this if you're using the standard UI Text
using UnityEngine.SceneManagement; // Required to get the active scene

public class ShowTextOnCollision : MonoBehaviour
{
    public Text popupText; // Reference to the Text component
    public GameObject popupBackground; // Reference to the background GameObject
    private bool isPlayerInRange = false; // Flag to check if the player is in range

    void Start()
    {
        // Ensure the text and background are hidden at the start
        if (popupText != null)
        {
            popupText.text = "";
        }
        if (popupBackground != null)
        {
            popupBackground.SetActive(false); // Hide the background
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;

            if (popupText != null)
            {
                popupText.text = "Press the space bar to enter ghost mode and pass through walls and other obstacles!\n(Press R to close)";
            }
            if (popupBackground != null)
            {
                popupBackground.SetActive(true); // Show the background
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Clear the text and hide the background when the player leaves the trigger area
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;

            if (popupText != null)
            {
                popupText.text = ""; // Clear the text
            }
            if (popupBackground != null)
            {
                popupBackground.SetActive(false); // Hide the background
            }
        }
    }

    void Update()
    {
        // Check for 'R' key press if the player is in range
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.R))
        {
            if (popupText != null)
            {
                popupText.text = ""; // Clear the text
            }
            if (popupBackground != null)
            {
                popupBackground.SetActive(false); // Hide the background
            }

            isPlayerInRange = false; // Reset the flag
        }
    }
}
