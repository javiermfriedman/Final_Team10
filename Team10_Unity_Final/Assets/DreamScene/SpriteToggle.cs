using UnityEngine;

public class SpriteToggle : MonoBehaviour
{
    public Sprite sprite1; // First sprite for toggle
    public Sprite sprite2; // Second sprite for toggle
    private SpriteRenderer spriteRenderer;
    private bool isSprite1Active = true;
    public GameObject screen; // Reference to the screen GameObject

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite1; // Set initial sprite

        if (screen != null)
        {
            screen.SetActive(false); // Start with screen off
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
        }
        else
        {
            spriteRenderer.sprite = sprite1;
        }

        isSprite1Active = !isSprite1Active; // Toggle the state

        // Toggle the screen's active state
        if (screen != null)
        {
            screen.SetActive(!screen.activeSelf);
        }
    }
}
