using UnityEngine;

public class SprintController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private HorizontalMovementController horizontalMovementController;

    [Header("Sprint Settings")]
    public float sprintMaxSpeedMultiplier = 2f;
    public float sprintStaminaReduction = 10f;
    public float sprintAccelerationMultiplier = 1.5f;
    public float sprintDecelerationMultiplier = 0.5f;
    private bool isSprinting = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && playerController.CurrentStamina >= sprintStaminaReduction)
        {
            StartSprinting();
        }
        else
        {
            StopSprinting();
        }

        if (isSprinting)
        {
            playerController.ReduceStamina(sprintStaminaReduction * Time.deltaTime);
        }
    }
    private void StartSprinting()
    {
        if (!isSprinting)
        {
            isSprinting = true;

            horizontalMovementController.horizontalMaxSpeed *= sprintMaxSpeedMultiplier;
            horizontalMovementController.AirborneAcceleration *= sprintAccelerationMultiplier;
            horizontalMovementController.GroundedAcceleration *= sprintAccelerationMultiplier;
            horizontalMovementController.AirborneDeceleration *= sprintDecelerationMultiplier;
            horizontalMovementController.GroundedDeceleration *= sprintDecelerationMultiplier;
        }
    }
    private void StopSprinting()
    {
        if (isSprinting)
        {
            isSprinting = false;

            horizontalMovementController.horizontalMaxSpeed /= sprintMaxSpeedMultiplier;
            horizontalMovementController.AirborneAcceleration /= sprintAccelerationMultiplier;
            horizontalMovementController.GroundedAcceleration /= sprintAccelerationMultiplier;
            horizontalMovementController.AirborneDeceleration /= sprintDecelerationMultiplier;
            horizontalMovementController.GroundedDeceleration /= sprintDecelerationMultiplier;
        }
    }
}