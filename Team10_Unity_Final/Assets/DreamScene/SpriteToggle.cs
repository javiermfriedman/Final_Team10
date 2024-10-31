using UnityEngine;

public class SpriteToggle : MonoBehaviour
{
    public Sprite sprite1; // Drag your first sprite here
    public Sprite sprite2; // Drag your second sprite here
    private SpriteRenderer spriteRenderer;
    private bool isSprite1Active = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite1; // Set initial sprite
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Check if spacebar is pressed
        {
            ToggleSprite();
        }
    }

    void ToggleSprite()
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
    }
}
