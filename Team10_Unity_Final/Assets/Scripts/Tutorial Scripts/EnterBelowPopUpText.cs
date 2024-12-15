using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Use this if you're using the standard UI Text
using UnityEngine.SceneManagement; // Required to get the active scene

public class EnterBelowPopUpText : MonoBehaviour
{
    public Text popupText; // Reference to the Text component
    public GameObject popupBackground; // Reference to the background GameObject
    private bool isPlayerInRange = false; // Flag to check if the player is in range

    void Start()
    {
        // Ensure the text and background are hidden at the start
        if (popupText != null)
        {
            popupText.text = ""; // Start with empty text
        }
        if (popupBackground != null)
        {
            popupBackground.SetActive(false); // Hide the background initially
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
                popupText.text = "What is this suspicious fragment... Maybe I should get near it!\n(Press R to close)";
            }
            if (popupBackground != null)
            {
                popupBackground.SetActive(true); // Show the background
            }
        }
    }

    // Uncomment if you want it to clear when the player exits
    // void OnTriggerExit2D(Collider2D other)
    // {
    //     // Clear the text and hide the background when the player leaves
    //     if (other.CompareTag("Player"))
    //     {
    //         isPlayerInRange = false;
    //         if (popupText != null)
    //         {
    //             popupText.text = "";
    //         }
    //         if (popupBackground != null)
    //         {
    //             popupBackground.SetActive(false);
    //         }
    //     }
    // }

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
