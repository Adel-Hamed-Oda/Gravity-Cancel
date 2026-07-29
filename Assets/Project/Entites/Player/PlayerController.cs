using System;
using System.Collections;
using UnityEngine;

public class PlayerController : Entity
{
    public event Action OnPlayerDeath;

    [Header("Stamina Settings")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaRegenDelay = 2f;
    [SerializeField] private float staminaRegenAccel = 5f;

    public float CurrentStamina { get; private set; }

    private Coroutine staminaRegenRoutine;

    private void Start()
    {
        CurrentStamina = maxStamina;
    }

    public void ReduceStamina(float amount)
    {
        CurrentStamina = Mathf.Max(0f, CurrentStamina - amount);

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
            rate += staminaRegenAccel * Time.deltaTime;
            CurrentStamina = Mathf.Min(maxStamina, CurrentStamina + rate);

            yield return null;
        }

        staminaRegenRoutine = null;
    }

    public void Die()
    {
        OnPlayerDeath?.Invoke();
    }
}