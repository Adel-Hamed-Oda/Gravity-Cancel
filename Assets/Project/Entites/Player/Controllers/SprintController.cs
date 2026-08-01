using UnityEngine;

public class SprintController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private HorizontalMovementController horizontalMovementController;
    [SerializeField] private GameObject sprintParticlesPrefab;

    [Header("Sprint Settings")]
    public float sprintMaxSpeedMultiplier = 2f;
    public float sprintStaminaReduction = 20f;
    public float sprintAccelerationMultiplier = 1.5f;
    public float sprintDecelerationMultiplier = 0.5f;

    public bool IsSprinting { get; private set; } = false;
    private ParticleSystem sprintParticlesInstance;

    // Cached base values to prevent stat compounding and float precision drift
    private float baseMaxSpeed;
    private float baseGroundedAcc;
    private float baseAirborneAcc;
    private float baseGroundedDec;
    private float baseAirborneDec;

    private void Awake()
    {
        CacheBaseValues();
    }

    private void CacheBaseValues()
    {
        if (horizontalMovementController != null)
        {
            baseMaxSpeed = horizontalMovementController.horizontalMaxSpeed;
            baseGroundedAcc = horizontalMovementController.GroundedAcceleration;
            baseAirborneAcc = horizontalMovementController.AirborneAcceleration;
            baseGroundedDec = horizontalMovementController.GroundedDeceleration;
            baseAirborneDec = horizontalMovementController.AirborneDeceleration;
        }
    }

    private void Update()
    {
        bool wantsToSprint = Input.GetKey(KeyCode.LeftShift);
        bool hasStamina = playerController != null && playerController.CurrentStamina >= sprintStaminaReduction * Time.deltaTime;

        // Use Mathf.Abs so moving left (negative speed) is properly detected!
        bool isSprinting = horizontalMovementController != null && Mathf.Abs(horizontalMovementController.CurrentHorizontalSpeed) >= horizontalMovementController.horizontalMaxSpeed;

        if (wantsToSprint && hasStamina && isSprinting)
        {
            StartSprinting();
        }
        else
        {
            StopSprinting();
        }

        if (IsSprinting)
        {
            playerController.ReduceStamina(sprintStaminaReduction * Time.deltaTime);
        }
    }

    private void StartSprinting()
    {
        if (!IsSprinting && horizontalMovementController != null)
        {
            IsSprinting = true;

            // Multiply cleanly off base values
            horizontalMovementController.horizontalMaxSpeed = baseMaxSpeed * sprintMaxSpeedMultiplier;
            horizontalMovementController.AirborneAcceleration = baseAirborneAcc * sprintAccelerationMultiplier;
            horizontalMovementController.GroundedAcceleration = baseGroundedAcc * sprintAccelerationMultiplier;
            horizontalMovementController.AirborneDeceleration = baseAirborneDec * sprintDecelerationMultiplier;
            horizontalMovementController.GroundedDeceleration = baseGroundedDec * sprintDecelerationMultiplier;

            SetParticleSystemActive(true);
        }
    }

    private void StopSprinting()
    {
        if (IsSprinting && horizontalMovementController != null)
        {
            IsSprinting = false;

            // Reset cleanly back to exact original base values
            horizontalMovementController.horizontalMaxSpeed = baseMaxSpeed;
            horizontalMovementController.AirborneAcceleration = baseAirborneAcc;
            horizontalMovementController.GroundedAcceleration = baseGroundedAcc;
            horizontalMovementController.AirborneDeceleration = baseAirborneDec;
            horizontalMovementController.GroundedDeceleration = baseAirborneDec;

            SetParticleSystemActive(false);
        }
    }

    private void SetParticleSystemActive(bool active)
    {
        if (sprintParticlesPrefab == null) return;

        if (sprintParticlesInstance == null)
        {
            GameObject instantiatedObj = Instantiate(sprintParticlesPrefab, transform);
            sprintParticlesInstance = instantiatedObj.GetComponent<ParticleSystem>();
        }

        if (sprintParticlesInstance != null)
        {
            ParticleSystem.EmissionModule emission = sprintParticlesInstance.emission;
            emission.enabled = active;
        }
    }

    private void OnDisable()
    {
        // Safety net: if script gets disabled (e.g. during teleport dash), reset stats immediately
        StopSprinting();
    }
}