using UnityEngine;

public class GroundedChecker : MonoBehaviour
{
    [Header("Ground Check Settings")]
    [SerializeField] private Transform feetPosition;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    public bool IsGrounded { get; private set; }

    private void Update()
    {
        CheckGrounded();
    }

    private void CheckGrounded()
    {
        if (feetPosition == null) return;

        IsGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckDistance, groundLayer) != null;
    }

    private void OnDrawGizmosSelected()
    {
        if (feetPosition == null) return;

        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(feetPosition.position, groundCheckDistance);
    }
}