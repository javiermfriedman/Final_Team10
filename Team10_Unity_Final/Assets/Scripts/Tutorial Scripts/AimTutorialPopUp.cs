using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Use this if you're using the standard UI Text
using UnityEngine.SceneManagement; // Required to get the active scene

public class AimTutorialPopUp : MonoBehaviour
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
                popupText.text = "Aim with mouse and left click to shoot enemies!\n(Press R to close)";
            }
            if (popupBackground != null)
            {
                popupBackground.SetActive(true); // Show the background
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
