using UnityEngine;

public class SpriteToggle : MonoBehaviour
{
    public Sprite sprite1; // First sprite for toggle
    public Sprite sprite2; // Second sprite for toggle
    private SpriteRenderer spriteRenderer;
    private bool isSprite1Active = true;
    public GameObject screen; // Reference to the screen GameObject

    public bool isGhostMode = false; // Boolean to track ghost mode
    public AudioSource ghostSound; // Reference to the AudioSource for ghost sound

    public string TagToIgnore = "Ignored";

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Check if spacebar is pressed
        {
            ToggleSpriteAndScreen();
        }
    }

    void ToggleSpriteAndScreen()
    {
        if (isSprite1Active)
        {
            spriteRenderer.sprite = sprite2;
            isGhostMode = true; // Set to ghost mode
            if (ghostSound != null)
            {
                ghostSound.Play(); // Play ghost sound when in ghost mode
            }
        }
        else
        {
            spriteRenderer.sprite = sprite1;
            isGhostMode = false; // Set to normal mode
            if (ghostSound != null)
            {
                ghostSound.Stop(); // Stop ghost sound when exiting ghost mode
            }
        }

        isSprite1Active = !isSprite1Active; // Toggle the state

        // Toggle the screen's active state
        if (screen != null)
        {
            screen.SetActive(!screen.activeSelf);
        }
    }

    // void OnCollisionEnter2D (Collision2D collision){
    //     if (collision.gameObject.tag == TagToIgnore && isGhostMode == true){
    //             Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    //     }
    // }
}
