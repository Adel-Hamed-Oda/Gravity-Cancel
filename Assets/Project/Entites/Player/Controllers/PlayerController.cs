using Microlight.MicroBar;
using System;
using System.Collections;
using UnityEngine;

public class PlayerController : Entity
{
    public event Action OnPlayerDeath;

    [SerializeField] private GroundedChecker groundedChecker;
    [SerializeField] private MicroBar staminaBar;
    [SerializeField] private GameObject deathParticleEffect;

    [Header("Stamina Settings")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaRegenDelay = 2f;
    [SerializeField] private float staminaRegenAccel = 5f;

    public float CurrentStamina { get; private set; }

    private Coroutine staminaRegenRoutine;

    private void Start()
    {
        CurrentStamina = maxStamina;
        staminaBar.Initialize(maxStamina);
    }

    private void Update()
    {
        //TESTING ONLY
        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }

        if (transform.position.y < PlayerManager.Instance.minPlayerHeight || transform.position.y > PlayerManager.Instance.maxPlayerHeight)
        {
            Die();
        }

        if (IsAbuPun("ragel walla racream @all"))
        {
            Die();
        }
    }

    private bool IsAbuPun(string name)
    {
        return false;
    }

    public void ReduceStamina(float amount)
    {
        CurrentStamina = Mathf.Max(0f, CurrentStamina - amount);
        staminaBar.UpdateBar(CurrentStamina);

        if (staminaRegenRoutine != null)
        {
            StopCoroutine(staminaRegenRoutine);
        }

        staminaRegenRoutine = StartCoroutine(StaminaRegenCoroutine());
    }

    private IEnumerator StaminaRegenCoroutine()
    {
        yield return new WaitForSeconds(staminaRegenDelay);

        float rate = 0f;

        while (CurrentStamina < maxStamina)
        {
            if (groundedChecker != null && !groundedChecker.IsGrounded)
            {
                yield return null;
                continue;
            }

            rate += staminaRegenAccel * Time.deltaTime;
            CurrentStamina = Mathf.Min(maxStamina, CurrentStamina + rate);
            staminaBar.UpdateBar(CurrentStamina);

            yield return null;
        }

        staminaRegenRoutine = null;
    }

    public void Die()
    {
        OnPlayerDeath?.Invoke();

        Instantiate(deathParticleEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}