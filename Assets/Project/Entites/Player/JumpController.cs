using UnityEngine;

public class JumpController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform feetPosition;

    [Header("Jump Settings")]
    public float JumpForce = 5.0f;
    public int MaxJumps = 1;
    public float GroundCheckDistance = 0.1f;
    public float JumpDelay = 0.1f;
    public float MaxVerticalSpeed = 10.0f;

    private int remainingJumps;
    private float jumpDelayTimer = 0.0f;

    public bool IsGrounded { get; private set; }

    private void Update()
    {
        jumpDelayTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            Jump();
        }

        CalculateGrounded();
        if (IsGrounded && jumpDelayTimer <= 0) // to ensure extra jumps don't happen
        {
            ResetJumps();
        }
    }

    private void CalculateGrounded()
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        IsGrounded = Physics2D.OverlapCircle(feetPosition.position, GroundCheckDistance, groundLayer) != null;
    }
    private bool CanJump()
    {
        return remainingJumps > 0 && jumpDelayTimer <= 0;
    }
    private void Jump()
    {
        rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        rb.linearVelocityY = Mathf.Clamp(rb.linearVelocity.y, -MaxVerticalSpeed, MaxVerticalSpeed);
        remainingJumps--;
        jumpDelayTimer = JumpDelay;
    }
    private void ResetJumps()
    {
        remainingJumps = MaxJumps;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(feetPosition.position, GroundCheckDistance);
    }
}