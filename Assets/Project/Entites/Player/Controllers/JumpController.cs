using UnityEngine;

public class JumpController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundedChecker groundedChecker;
    [SerializeField] private GameObject jumpParticlesPrefab;

    [Header("Jump Settings")]
    public float jumpForce = 5.0f;
    public int maxAirJumps = 1; // Renamed to clarify these are strictly air jumps
    public float jumpDelay = 0.1f;
    public float maxVerticalSpeed = 10.0f;

    private int remainingAirJumps;
    private float jumpDelayTimer = 0.0f;

    private void Update()
    {
        jumpDelayTimer -= Time.deltaTime;

        // Reset air jumps when grounded and not in the middle of a jump delay
        if (groundedChecker != null && groundedChecker.IsGrounded && jumpDelayTimer <= 0)
        {
            ResetAirJumps();
        }

        // Jump Input Check
        if (Input.GetKeyDown(KeyCode.Space) && jumpDelayTimer <= 0)
        {
            if (groundedChecker != null && groundedChecker.IsGrounded)
            {
                // Perform a Grounded Jump
                PerformJump(isGroundedJump: true);
            }
            else if (remainingAirJumps > 0)
            {
                // Perform an Air Jump
                PerformJump(isGroundedJump: false);
            }
        }
    }

    private void PerformJump(bool isGroundedJump)
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // Clamp vertical velocity while preserving horizontal velocity
        float clampedY = Mathf.Clamp(rb.linearVelocity.y, -maxVerticalSpeed, maxVerticalSpeed);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, clampedY);

        if (jumpParticlesPrefab != null)
        {
            Instantiate(jumpParticlesPrefab, transform);
        }

        // Only consume a jump charge if it was an air jump
        if (!isGroundedJump)
        {
            remainingAirJumps--;
        }

        jumpDelayTimer = jumpDelay;
    }

    private void ResetAirJumps()
    {
        remainingAirJumps = maxAirJumps;
    }
}