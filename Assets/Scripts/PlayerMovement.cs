using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    public float sideSlide = 50f;
    public float yoPower = 5f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform grounderCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform yoCheck;
    [SerializeField] private LayerMask yoLayer;
    [SerializeField] private Transform slideCheck;
    [SerializeField] private LayerMask slideLayer;
    [SerializeField] private LayerMask deathLayer;
    [SerializeField] private Transform deathCheck;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded() || Input.GetButtonDown("Jump") && IsSlide() || Input.GetButtonDown("Jump") && IsGrounder())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (IsSlide())
        {
            rb.velocity = new Vector2(rb.velocity.x+sideSlide, 0f);
        }

        if (IsYo())
        {
            rb.velocity = new Vector2(rb.velocity.x, yoPower);
        }

        if (IsDeath())
        {
            rb.velocity = new Vector2(0f, 0f);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();

    }
    //speed
    private void FixedUpdate()
    {
        if (IsGrounded() || IsGrounder())
        {
            rb.velocity = new Vector2(horizontal, rb.velocity.y);
        }
        if (IsSlide())
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        if (IsDeath())
        {
            gameObject.transform.position = new Vector2(0f, 0f);
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }


    //The Groundcheck
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsGrounder()
    {
        return Physics2D.OverlapCircle(grounderCheck.position, 0.2f, groundLayer);
    }
    
    private bool IsYo()
    {
        return Physics2D.OverlapCircle(yoCheck.position, 0.2f, yoLayer);
    }

    private bool IsSlide()
    {
        return Physics2D.OverlapCircle(slideCheck.position, 0.2f, slideLayer);
    }

    private bool IsDeath()
    {
        return Physics2D.OverlapCircle(deathCheck.position, 0.2f, deathLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}