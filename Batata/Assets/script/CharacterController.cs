using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Sprite spriteFacingLeft;
    public Sprite spriteFacingRight;
    public Sprite spriteFacingFront;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the SpriteRenderer component attached to the character
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Detect input and move the character
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Update the character's position
        transform.Translate(new Vector3(moveX, moveY, 0) * Time.deltaTime * 5f);

        // Change the sprite based on the direction
        if (moveX < 0)
        {
            // Facing Left
            spriteRenderer.sprite = spriteFacingLeft;
        }
        else if (moveX > 0)
        {
            // Facing Right
            spriteRenderer.sprite = spriteFacingRight;
        }
        else if (moveY < 0)
        {
            // Facing Front
            spriteRenderer.sprite = spriteFacingFront;
        }
    }
}
