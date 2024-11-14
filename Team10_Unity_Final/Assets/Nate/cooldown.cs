using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    public Sprite sprite1; // First sprite for toggle
    public Sprite sprite2; // Second sprite for toggle
    private SpriteRenderer spriteRenderer;
    private bool isSprite1Active = true;
    public GameObject screen; // Reference to the screen GameObject

    public bool isGhostMode = false; // Boolean to track ghost mode
    public AudioSource ghostSound; // Reference to the AudioSource for ghost sound
    private Collider2D wallCollider; // Reference to the wall's collider

    public float ghostModeDuration = 5f; // Duration for ghost mode
    public float cooldownDuration = 5f;   // Cooldown period after ghost mode ends
    private float ghostModeTimer = 0f;
    private float cooldownTimer = 0f;
    private bool canEnterGhostMode = true; // Flag to check if ghost mode can be entered

    public Image cooldownImage; // Reference to the UI Image for the cooldown indicator

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite1; // Set initial sprite

        if (screen != null)
        {
            screen.SetActive(false); // Start with screen off
        }

        if (ghostSound != null)
        {
            ghostSound.Stop(); // Ensure sound is off at the start
        }

        if (cooldownImage != null)
        {
            cooldownImage.transform.localScale = Vector3.one; // Set the image to full size initially
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canEnterGhostMode) // Check if spacebar is pressed and cooldown is over
        {
            if (!isGhostMode)
            {
                EnterGhostMode();
            }
            else
            {
                ExitGhostMode();
            }
        }

        // Handle ghost mode timer countdown
        if (isGhostMode)
        {
            ghostModeTimer -= Time.deltaTime;
            UpdateCooldownImage(ghostModeTimer, ghostModeDuration);

            if (ghostModeTimer <= 0)
            {
                ExitGhostMode(); // Exit ghost mode when timer reaches 0
            }
        }

        // Handle cooldown timer countdown if in cooldown
        if (!canEnterGhostMode)
        {
            cooldownTimer -= Time.deltaTime;
            UpdateCooldownImage(cooldownDuration-cooldownTimer, cooldownDuration);

            if (cooldownTimer <= 0)
            {
                canEnterGhostMode = true; // Allow ghost mode to be entered again after cooldown
                UpdateCooldownImage(1, 1); // Reset the cooldown image to full size
            }
        }
    }

    void EnterGhostMode()
    {
        isGhostMode = true;
        ghostModeTimer = ghostModeDuration; // Reset ghost mode timer
        ToggleSpriteAndScreen();
    }

    void ExitGhostMode()
    {
        isGhostMode = false;
        canEnterGhostMode = false; // Prevent re-entry until cooldown is over
        cooldownTimer = cooldownDuration - ghostModeTimer; // Start cooldown timer
        ToggleSpriteAndScreen();
    }

    void ToggleSpriteAndScreen()
    {
        if (isSprite1Active)
        {
            spriteRenderer.sprite = sprite2;
            Debug.Log("ghost mode true");
            if (ghostSound != null)
            {
                ghostSound.Play(); // Play ghost sound when in ghost mode
            }
        }
        else
        {
            spriteRenderer.sprite = sprite1;
            Debug.Log("ghost mode false");
            if (ghostSound != null)
            {
                ghostSound.Stop(); // Stop ghost sound when exiting ghost mode
            }

            // Re-enable collision with the wall when exiting ghost mode
            if (wallCollider != null)
            {
                Physics2D.IgnoreCollision(wallCollider, GetComponent<Collider2D>(), false);
            }
        }

        isSprite1Active = !isSprite1Active; // Toggle the state

        // Toggle the screen's active state
        if (screen != null)
        {
            screen.SetActive(!screen.activeSelf);
        }
    }

    void UpdateCooldownImage(float currentTimer, float maxDuration)
    {
        if (cooldownImage != null)
        {
            // Calculate the proportional scale based on the remaining time
            float scale = Mathf.Clamp01(currentTimer / maxDuration);
            cooldownImage.transform.localScale = new Vector3(scale, 1, 1);

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ignored")
        {
            wallCollider = collision.collider; // Store reference to wall collider
            Debug.Log("Hitting wall");
            
            if (isGhostMode)
            {
                Debug.Log("Ignoring collision with wall in ghost mode");
                Physics2D.IgnoreCollision(wallCollider, GetComponent<Collider2D>(), true);
            }
        }
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Cooldown : MonoBehaviour
// {
//     public Sprite sprite1; // First sprite for toggle
//     public Sprite sprite2; // Second sprite for toggle
//     private SpriteRenderer spriteRenderer;
//     private bool isSprite1Active = true;
//     public GameObject screen; // Reference to the screen GameObject

//     public bool isGhostMode = false; // Boolean to track ghost mode
//     public AudioSource ghostSound; // Reference to the AudioSource for ghost sound
//     private Collider2D wallCollider; // Reference to the wall's collider

//     public float ghostModeDuration = 5f; // Duration for ghost mode
//     public float cooldownDuration = 5f;   // Cooldown period after ghost mode ends
//     private float ghostModeTimer = 0f;
//     private float cooldownTimer = 0f;
//     private bool canEnterGhostMode = true; // Flag to check if ghost mode can be entered

//     void Start()
//     {
//         spriteRenderer = GetComponent<SpriteRenderer>();
//         spriteRenderer.sprite = sprite1; // Set initial sprite

//         if (screen != null)
//         {
//             screen.SetActive(false); // Start with screen off
//         }

//         if (ghostSound != null)
//         {
//             ghostSound.Stop(); // Ensure sound is off at the start
//         }
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Space) && canEnterGhostMode) // Check if spacebar is pressed and cooldown is over
//         {
//             if (!isGhostMode)
//             {
//                 EnterGhostMode();
//             }
//             else
//             {
//                 ExitGhostMode();
//             }
//         }

//         // Handle ghost mode timer countdown
//         if (isGhostMode)
//         {
//             ghostModeTimer -= Time.deltaTime;
//             if (ghostModeTimer <= 0)
//             {
//                 ExitGhostMode(); // Exit ghost mode when timer reaches 0
//             }
//         }

//         // Handle cooldown timer countdown if in cooldown
//         if (!canEnterGhostMode)
//         {
//             cooldownTimer -= Time.deltaTime;
//             if (cooldownTimer <= 0)
//             {
//                 canEnterGhostMode = true; // Allow ghost mode to be entered again after cooldown
//             }
//         }
//     }

//     void EnterGhostMode()
//     {
//         isGhostMode = true;
//         ghostModeTimer = ghostModeDuration; // Reset ghost mode timer
//         ToggleSpriteAndScreen();
//     }

//     void ExitGhostMode()
//     {
//         isGhostMode = false;
//         canEnterGhostMode = false; // Prevent re-entry until cooldown is over
//         cooldownTimer = cooldownDuration; // Start cooldown timer
//         ToggleSpriteAndScreen();
//     }

//     void ToggleSpriteAndScreen()
//     {
//         if (isSprite1Active)
//         {
//             spriteRenderer.sprite = sprite2;
//             Debug.Log("ghost mode true");
//             if (ghostSound != null)
//             {
//                 ghostSound.Play(); // Play ghost sound when in ghost mode
//             }
//         }
//         else
//         {
//             spriteRenderer.sprite = sprite1;
//             Debug.Log("ghost mode false");
//             if (ghostSound != null)
//             {
//                 ghostSound.Stop(); // Stop ghost sound when exiting ghost mode
//             }

//             // Re-enable collision with the wall when exiting ghost mode
//             if (wallCollider != null)
//             {
//                 Physics2D.IgnoreCollision(wallCollider, GetComponent<Collider2D>(), false);
//             }
//         }

//         isSprite1Active = !isSprite1Active; // Toggle the state

//         // Toggle the screen's active state
//         if (screen != null)
//         {
//             screen.SetActive(!screen.activeSelf);
//         }
//     }

//     void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.gameObject.tag == "Ignored")
//         {
//             wallCollider = collision.collider; // Store reference to wall collider
//             Debug.Log("Hitting wall");
            
//             if (isGhostMode)
//             {
//                 Debug.Log("Ignoring collision with wall in ghost mode");
//                 Physics2D.IgnoreCollision(wallCollider, GetComponent<Collider2D>(), true);
//             }
//         }
//     }
// }
