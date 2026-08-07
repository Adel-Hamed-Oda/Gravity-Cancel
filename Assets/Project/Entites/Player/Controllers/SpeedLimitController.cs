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

        Vector2 currentVelocity = rb.linearVelocity;
        float clampedX = Mathf.Clamp(currentVelocity.x, -maxLeftSpeed, maxRightSpeed);
        float clampedY = Mathf.Clamp(currentVelocity.y, -maxDownSpeed, maxUpSpeed);

        rb.linearVelocity = new Vector2(clampedX, clampedY);
    }
}