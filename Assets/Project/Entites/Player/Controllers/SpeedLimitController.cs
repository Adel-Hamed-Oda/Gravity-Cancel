using UnityEngine;

public class SpeedLimitController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [Header("Vertical Limits")]
    public float maxUpSpeed = 10f;
    public float maxDownSpeed = 10f;

    [Header("Horizontal Limits")]
    public float maxRightSpeed = 10f;
    public float maxLeftSpeed = 10f;

    private void FixedUpdate()
    {
        if (rb == null) return;

        Vector2 currentVelocity = rb.linearVelocity; // Use rb.velocity if using Unity 2022 or older

        // Clamp Horizontal Velocity (-maxLeft to +maxRight)
        float clampedX = Mathf.Clamp(currentVelocity.x, -maxLeftSpeed, maxRightSpeed);

        // Clamp Vertical Velocity (-maxDown to +maxUp)
        float clampedY = Mathf.Clamp(currentVelocity.y, -maxDownSpeed, maxUpSpeed);

        // Reapply clamped velocity
        rb.linearVelocity = new Vector2(clampedX, clampedY);
    }
}