using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private enum AnimationState
    {
        Idle,
        Walking,
        Sprinting,
        Falling
    }

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundedChecker groundedChecker;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SprintController sprintController;

    [Header("Settings")]
    [SerializeField] private float movementThreshold = 0.2f;

    private AnimationState currentState;

    private void Start()
    {
        currentState = GetCurrentState();
        animator.SetTrigger(currentState.ToString());
    }

    private void Update()
    {
        UpdateAnimationState();
        UpdateSpriteDirection();
    }

    private void UpdateAnimationState()
    {
        AnimationState newState = GetCurrentState();

        if (newState == currentState)
            return;

        currentState = newState;
        animator.SetTrigger(currentState.ToString());
    }

    private AnimationState GetCurrentState()
    {
        if (!groundedChecker.IsGrounded)
            return AnimationState.Falling;

        if (sprintController.IsSprinting)
            return AnimationState.Sprinting;

        if (Mathf.Abs(rb.linearVelocity.x) > movementThreshold)
            return AnimationState.Walking;

        return AnimationState.Idle;
    }

    private void UpdateSpriteDirection()
    {
        float horizontalVelocity = rb.linearVelocity.x;

        if (Mathf.Abs(horizontalVelocity) > 0.01f)
            spriteRenderer.flipX = horizontalVelocity < 0;
    }
}