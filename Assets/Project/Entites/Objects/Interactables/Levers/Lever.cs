using UnityEngine;
// using UnityEngine.UI; // Removed unless this is actually on a UI Canvas

public class Lever : SignalProvider
{
    [Header("References")]
    [SerializeField] private SpriteRenderer leverSpriteRenderer;
    [SerializeField] private Sprite activatedLever;
    [SerializeField] private Sprite deactivatedLever;

    [Header("Lever States")]
    [SerializeField] private bool isActivated = false;
    [SerializeField] private bool isToggleable = true;

    private void Start()
    {
        UpdateLeverSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isToggleable && isActivated) return;

            ToggleLever();
        }
    }

    private void ToggleLever()
    {
        isActivated = !isActivated;

        if (isActivated)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }

        UpdateLeverSprite();
    }

    private void UpdateLeverSprite()
    {
        if (leverSpriteRenderer == null) return;

        if (isActivated)
        {
            leverSpriteRenderer.sprite = activatedLever;
        }
        else
        {
            leverSpriteRenderer.sprite = deactivatedLever;
        }
    }
}