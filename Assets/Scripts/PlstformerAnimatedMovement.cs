using UnityEngine;

public class PlatformerAnimatedMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    public SpriteRenderer spriteRenderer;

    public Sprite[] walkRightSprites;
    public Sprite[] walkLeftSprites;

    public float animationSpeed = 0.2f;

    private Rigidbody2D rb;
    private bool isGrounded = false;

    private float animationTimer = 0f;
    private int frameIndex = 0;

    private int lastDirection = 1; 
    // 1 = right, -1 = left

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        if (moveX > 0)
        {
            lastDirection = 1;
            AnimateWalk(walkRightSprites);
        }
        else if (moveX < 0)
        {
            lastDirection = -1;
            AnimateWalk(walkLeftSprites);
        }
        else
        {
            ShowIdleSprite();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void AnimateWalk(Sprite[] sprites)
    {
        if (sprites == null || sprites.Length == 0)
            return;

        animationTimer += Time.deltaTime;

        if (animationTimer >= animationSpeed)
        {
            animationTimer = 0f;
            frameIndex++;

            if (frameIndex >= sprites.Length)
            {
                frameIndex = 0;
            }

            spriteRenderer.sprite = sprites[frameIndex];
        }
    }

    void ShowIdleSprite()
    {
        animationTimer = 0f;
        frameIndex = 0;

        if (lastDirection == 1)
        {
            if (walkRightSprites != null && walkRightSprites.Length > 0)
            {
                spriteRenderer.sprite = walkRightSprites[0];
            }
        }
        else
        {
            if (walkLeftSprites != null && walkLeftSprites.Length > 0)
            {
                spriteRenderer.sprite = walkLeftSprites[0];
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}