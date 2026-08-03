using System.Collections;
using UnityEngine;

public class DashController : MonoBehaviour
{
    [SerializeField] private HorizontalMovementController horizontalMovementController;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundedChecker groundChecker;
    [SerializeField] private GameObject dashParticlesPrefab;

    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashCooldown = 1f;
    public int maxDashes = 1;

    private float dashCooldownTimer = 0f;
    private int currentDashes;

    private void Start()
    {
        // Initialize with full dashes
        currentDashes = maxDashes;
    }

    private void Update()
    {
        dashCooldownTimer -= Time.deltaTime;

        if (groundChecker != null && groundChecker.IsGrounded)
        {
            currentDashes = maxDashes;
        }

        if (Input.GetKey(KeyCode.Z) && dashCooldownTimer <= 0 && currentDashes > 0)
        {
            Dash();
        }
    }

    private void Dash()
    {
        if (horizontalMovementController == null) return;

        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = -Camera.main.transform.position.z;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        Vector2 offset = mouseWorldPosition - transform.position;
        Vector2 dashDirection;
        dashDirection = offset.x > 0 ? Vector2.right : Vector2.left;

        rb.linearVelocity = Vector2.zero;
        horizontalMovementController.CurrentHorizontalSpeed = dashDirection.x * dashSpeed;

        dashCooldownTimer = dashCooldown;
        currentDashes--;

        GameObject dashParticles = Instantiate(dashParticlesPrefab, transform);
        dashParticles.transform.localScale = new Vector3(dashDirection.x, dashParticles.transform.localScale.y, dashParticles.transform.localScale.z);
    }
}