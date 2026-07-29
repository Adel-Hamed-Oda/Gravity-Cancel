using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundedChecker groundChecker;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Update()
    {
        animator.SetBool("Falling", !groundChecker.IsGrounded);
        animator.SetBool("Idle", Mathf.Abs(rb.linearVelocity.x) <= 0.2f && groundChecker.IsGrounded);
        animator.SetBool("Walking", Mathf.Abs(rb.linearVelocity.x) > 0.2f && groundChecker.IsGrounded);

        spriteRenderer.flipX = rb.linearVelocity.x < 0;
    }
}
