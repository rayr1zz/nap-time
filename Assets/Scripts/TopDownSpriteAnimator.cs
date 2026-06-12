using UnityEngine;

public class TopDownSpriteAnimator : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite[] walkDownSprites;
    public Sprite[] walkUpSprites;
    public Sprite[] walkLeftSprites;
    public Sprite[] walkRightSprites;

    public float animationSpeed = 0.18f;

    private Vector2 lastDirection = Vector2.down;
    private Vector2 movement;
    private float timer;
    private int frameIndex;

    void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.magnitude > 0.1f)
        {
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                lastDirection = movement.x > 0 ? Vector2.right : Vector2.left;
            }
            else
            {
                lastDirection = movement.y > 0 ? Vector2.up : Vector2.down;
            }

            AnimateMovement();
        }
        else
        {
            ShowIdleSprite();
        }
    }

    void AnimateMovement()
    {
        timer += Time.deltaTime;

        if (timer >= animationSpeed)
        {
            timer = 0f;
            frameIndex++;

            Sprite[] currentSprites = GetCurrentDirectionSprites();

            if (currentSprites.Length > 0)
            {
                frameIndex %= currentSprites.Length;
                spriteRenderer.sprite = currentSprites[frameIndex];
            }
        }
    }

    void ShowIdleSprite()
    {
        Sprite[] currentSprites = GetCurrentDirectionSprites();

        if (currentSprites.Length > 0)
        {
            spriteRenderer.sprite = currentSprites[0];
        }

        frameIndex = 0;
        timer = 0f;
    }

    Sprite[] GetCurrentDirectionSprites()
    {
        if (lastDirection == Vector2.up)
            return walkUpSprites;

        if (lastDirection == Vector2.down)
            return walkDownSprites;

        if (lastDirection == Vector2.left)
            return walkLeftSprites;

        if (lastDirection == Vector2.right)
            return walkRightSprites;

        return walkDownSprites;
    }
}