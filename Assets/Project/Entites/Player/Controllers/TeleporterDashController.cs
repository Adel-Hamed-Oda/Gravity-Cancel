using System.Collections;
using UnityEngine;

public class TeleportDashController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundedChecker groundChecker; // Added reference to GroundChecker

    [Header("Controllers to Disable")]
    [SerializeField] private HorizontalMovementController horizontalMovementController;
    [SerializeField] private JumpController jumpController;
    [SerializeField] private DashController dashController;

    [Header("Teleport Settings")]
    public float maxTeleportDistance = 6f;
    public float maxWallThickness = 3f;
    public float minTeleportDistance = 3f;
    public float wallOffset = 0.5f;
    public float teleportChargeTime = 0.5f;
    public float teleportCooldown = 1f;
    public int maxTeleportDashes = 1; // Replaced teleportStaminaReduction with a max counter
    public LayerMask obstacleLayer;

    [Header("Visuals")]
    [SerializeField] private GameObject implosionPrefab;
    [SerializeField] private GameObject explosionPrefab;

    private float cooldownTimer = 0f;
    private bool isTeleporting = false;
    private int currentTeleportDashes;

    private void Start()
    {
        // Initialize with full teleport dashes
        currentTeleportDashes = maxTeleportDashes;
    }

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // Reset the teleport dash counter when the player touches the ground
        if (groundChecker != null && groundChecker.IsGrounded && !isTeleporting)
        {
            currentTeleportDashes = maxTeleportDashes;
        }

        // Teleport condition: Input pressed, cooldown is over, not already teleporting, and we have charges left
        if (Input.GetKeyDown(KeyCode.C) && cooldownTimer <= 0 && !isTeleporting && currentTeleportDashes > 0)
        {
            AttemptTeleport();
        }
    }

    private void AttemptTeleport()
    {
        // 1. Determine direction based on mouse position
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = -Camera.main.transform.position.z;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        Vector2 offset = mouseWorldPosition - transform.position;
        Vector2 dashDirection = offset.x > 0 ? Vector2.right : Vector2.left;

        // 2. Start checking from max distance away, casting BACK towards the player
        Vector2 checkOrigin = (Vector2)transform.position + (dashDirection * maxTeleportDistance);
        Vector2 castDirection = -dashDirection;

        RaycastHit2D hit = Physics2D.Raycast(checkOrigin, castDirection, maxTeleportDistance, obstacleLayer);
        Vector2 targetPosition;

        if (hit.collider != null)
        {
            if (hit.distance <= 0.1f)
            {
                // Hit immediately (<= 0.1 distance). This means the max distance mark is inside a wall. 
                // Teleport fails completely.
                return;
            }
            else if (hit.distance <= maxWallThickness)
            {
                // Hit the back of a wall after a short distance (<= maxWallThickness).
                // Add the offset so the player spawns safely past the wall's surface.
                targetPosition = hit.point + (dashDirection * wallOffset);
            }
            else
            {
                // Hit a wall further than maxWallThickness away. 
                // This means the wall is very thin, or we are just in open space past it.
                targetPosition = (Vector2)transform.position + (dashDirection * minTeleportDistance);
            }
        }
        else
        {
            // No wall hit at all (completely open space).
            targetPosition = (Vector2)transform.position + (dashDirection * minTeleportDistance);
        }

        // 3. If valid, start the sequence
        StartCoroutine(TeleportSequence(targetPosition, dashDirection));
    }

    private IEnumerator TeleportSequence(Vector2 destination, Vector2 direction)
    {
        Debug.Log("Destination: " + destination + ", Direction: " + direction);

        isTeleporting = true;
        currentTeleportDashes--; // Consume one teleport charge here

        // Disable other movement controllers
        if (horizontalMovementController != null) horizontalMovementController.enabled = false;
        if (jumpController != null) jumpController.enabled = false;
        if (dashController != null) dashController.enabled = false;

        // Stop current momentum and gravity so the player hangs in the air
        rb.linearVelocity = Vector2.zero;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        // Spawn Implosion
        if (implosionPrefab != null)
        {
            GameObject implosion = Instantiate(implosionPrefab, transform.position, Quaternion.identity);
            implosion.transform.localScale = new Vector3(direction.x, implosion.transform.localScale.y, implosion.transform.localScale.z);
            Destroy(implosion, teleportChargeTime);
        }

        // Wait for charge time
        yield return new WaitForSeconds(teleportChargeTime);

        // Apply Teleport
        playerController.transform.position = destination;

        // Spawn Explosion
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, destination, Quaternion.identity);
            explosion.transform.localScale = new Vector3(direction.x, explosion.transform.localScale.y, explosion.transform.localScale.z);
        }

        // Restore physics and controllers
        rb.gravityScale = originalGravity;
        if (horizontalMovementController != null) horizontalMovementController.enabled = true;
        if (jumpController != null) jumpController.enabled = true;
        if (dashController != null) dashController.enabled = true;

        cooldownTimer = teleportCooldown;
        isTeleporting = false;
    }
}