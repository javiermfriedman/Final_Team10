using UnityEngine;

public class MeowSound : MonoBehaviour
{
    public AudioClip musicClip; // Reference to the music audio clip
    public float interval = 30f; // Interval in seconds between plays

    private AudioSource audioSource; // AudioSource component
    private float timer; // Timer to track time

    void Start()
    {
        // Add an AudioSource component if not already attached
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.playOnAwake = false; // Ensure it doesn't play automatically at start

        // Initialize the timer
        timer = interval;
    }

    void Update()
    {
        // Count down the timer
        timer -= Time.deltaTime;

        // Check if the timer has reached zero
        if (timer <= 0f)
        {
            PlayMusic();
            timer = interval; // Reset the timer
        }
    }

    void PlayMusic()
    {
        if (musicClip != null)
        {
            audioSource.Play(); // Play the audio clip
            Debug.Log("Music played!");
        }
        else
        {
            Debug.LogWarning("No music clip assigned!");
        }
    }
}

