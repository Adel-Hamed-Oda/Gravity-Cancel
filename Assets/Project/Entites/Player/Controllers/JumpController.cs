using UnityEngine;

public class JumpController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundedChecker groundedChecker;
    [SerializeField] private GameObject jumpParticlesPrefab;

    [Header("Jump Settings")]
    public float jumpForce = 5.0f;
    public int maxJumps = 1;
    public float jumpDelay = 0.1f;
    public float maxVerticalSpeed = 10.0f;

    private int remainingJumps;
    private float jumpDelayTimer = 0.0f;

    private void Update()
    {
        jumpDelayTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            Jump();
        }

        if (groundedChecker != null && groundedChecker.IsGrounded && jumpDelayTimer <= 0)
        {
            ResetJumps();
        }
    }

    private bool CanJump()
    {
        return remainingJumps > 0 && jumpDelayTimer <= 0;
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // Clamp vertical velocity while preserving horizontal velocity
        float clampedY = Mathf.Clamp(rb.linearVelocity.y, -maxVerticalSpeed, maxVerticalSpeed);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, clampedY);

        Instantiate(jumpParticlesPrefab, transform);

        remainingJumps--;
        jumpDelayTimer = jumpDelay;
    }

    private void ResetJumps()
    {
        remainingJumps = maxJumps;
    }
}