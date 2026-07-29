using System.Collections;
using UnityEngine;

public class DashController : MonoBehaviour
{
    [SerializeField] private HorizontalMovementController horizontalMovementController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject dashParticlesPrefab;

    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashCooldown = 1f;
    public float dashStaminaReduction = 25f;

    private float dashCooldownTimer = 0f;

    private void Update()
    {
        dashCooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Z) && dashCooldownTimer <= 0)
        {
            Dash();
        }
    }

    private void Dash()
    {
        if (mainCamera == null || horizontalMovementController == null) return;

        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = -mainCamera.transform.position.z;
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        Vector2 offset = mouseWorldPosition - transform.position;
        Vector2 dashDirection;
        dashDirection = offset.x > 0 ? Vector2.right : Vector2.left;

        horizontalMovementController.CurrentHorizontalSpeed = dashDirection.x * dashSpeed;
        dashCooldownTimer = dashCooldown;

        Instantiate(dashParticlesPrefab, transform.position, Quaternion.identity);
    }
}