using UnityEngine;

public class HorizontalMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private JumpController jumpController;

    [Header("Movement Settings")]
    public float horizontalMaxSpeed = 5.0f;
    public float GroundedAcceleration = 15.0f;
    public float AirborneAcceleration = 5.0f;
    public float GroundedDeceleration = 20.0f;
    public float AirborneDeceleration = 15.0f;

    private float horizontalInput;
    private float currentHorizontalSpeed;

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        float targetSpeed = horizontalInput * horizontalMaxSpeed;
        float chosenRate;

        // Check if input is in the opposite direction of current movement
        bool isChangingDirection = (horizontalInput != 0) && (currentHorizontalSpeed * horizontalInput < 0);

        if (isChangingDirection)
        {
            // Rapid turn-around braking rate
            chosenRate = GetDeceleration() + GetAcceleration();
        }
        else if (horizontalInput == 0)
        {
            // Coasting to a stop
            chosenRate = GetDeceleration();
        }
        else
        {
            // Normal acceleration
            chosenRate = GetAcceleration();
        }

        currentHorizontalSpeed = Mathf.MoveTowards(
            currentHorizontalSpeed,
            targetSpeed,
            chosenRate * Time.fixedDeltaTime
        );

        rb.linearVelocity = new Vector2(currentHorizontalSpeed, rb.linearVelocity.y);
    }

    private float GetAcceleration()
    {
        if (jumpController != null && !jumpController.IsGrounded)
        {
            return AirborneAcceleration;
        }
        else
        {
            return GroundedAcceleration;
        }
    }
    private float GetDeceleration()
    {
        if (jumpController != null && !jumpController.IsGrounded)
        {
            return AirborneDeceleration;
        }
        else
        {
            return GroundedDeceleration;
        }
    }
}