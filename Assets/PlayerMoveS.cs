using UnityEngine;

public class PlayerMoveControls : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 5f;

    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public float rayLength = 0.2f;
    public LayerMask groundLayer;

    private int direction = 1;
    private bool grounded = false;

    private GatherInput gatherInput;
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        gatherInput = GetComponent<GatherInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckGroundStatus();
        SetAnimatorValues();
        FlipCharacter();

        // Horizontal movement
        rb.velocity = new Vector2(speed * gatherInput.valueX, rb.velocity.y);

        HandleJump();
    }

    private void CheckGroundStatus()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            groundCheckPoint.position,
            Vector2.down,
            rayLength,
            groundLayer
        );

        grounded = hit.collider != null;
    }

    private void HandleJump()
    {
        if (gatherInput.jumpInput && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Reset jump input to avoid multiple jumps per press
        gatherInput.jumpInput = false;
    }

    private void SetAnimatorValues()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("VSpeed", rb.velocity.y);
        animator.SetBool("Ground", grounded);
    }

    /// <summary>
    /// Flip the character sprite when changing direction
    /// </summary>
    private void FlipCharacter()
    {
        if (gatherInput.valueX * direction < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
            direction *= -1;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw ray in editor to visualize ground check
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheckPoint.position,
                            groundCheckPoint.position + Vector3.down * rayLength);
        }
    }
}
