using UnityEngine;

public class HorizontalMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundedChecker groundedChecker;

    [Header("Movement Settings")]
    public float horizontalMaxSpeed = 5.0f;
    public float GroundedAcceleration = 15.0f;
    public float AirborneAcceleration = 5.0f;
    public float GroundedDeceleration = 20.0f;
    public float AirborneDeceleration = 15.0f;

    private float horizontalInput;

    [HideInInspector] public float CurrentHorizontalSpeed;

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        float targetSpeed = horizontalInput * horizontalMaxSpeed;
        float chosenRate;

        bool isChangingDirection = (horizontalInput != 0) && (CurrentHorizontalSpeed * horizontalInput < 0);

        if (isChangingDirection)
        {
            chosenRate = GetDeceleration() + GetAcceleration();
        }
        else if (horizontalInput == 0)
        {
            chosenRate = GetDeceleration();
        }
        else
        {
            chosenRate = GetAcceleration();
        }

        CurrentHorizontalSpeed = Mathf.MoveTowards(
            CurrentHorizontalSpeed,
            targetSpeed,
            chosenRate * Time.fixedDeltaTime
        );

        rb.linearVelocity = new Vector2(CurrentHorizontalSpeed, rb.linearVelocity.y);
    }

    private float GetAcceleration()
    {
        return (groundedChecker != null && !groundedChecker.IsGrounded) ? AirborneAcceleration : GroundedAcceleration;
    }

    private float GetDeceleration()
    {
        return (groundedChecker != null && !groundedChecker.IsGrounded) ? AirborneDeceleration : GroundedDeceleration;
    }
}