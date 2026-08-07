using System;
using UnityEngine;

public class Button : SignalProvider
{
    [Header("References")]
    [SerializeField] private SpriteRenderer pressurePlateSpriteRenderer;
    [SerializeField] private Sprite activatedPressurePlate;
    [SerializeField] private Sprite deactivatedPressurePlate;

    private bool isActivated = false;
    private int objectsOnButton = 0;

    private void Start()
    {
        UpdateButtonSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectsOnButton++;

        if (objectsOnButton > 0 && !isActivated)
        {
            isActivated = true;
            Activate();
            UpdateButtonSprite();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectsOnButton--;

        if (objectsOnButton < 0)
        {
            objectsOnButton = 0;
        }

        if (objectsOnButton == 0 && isActivated)
        {
            isActivated = false;
            Deactivate();
            UpdateButtonSprite();
        }
    }

    private void UpdateButtonSprite()
    {
        if (pressurePlateSpriteRenderer == null) return;

        if (isActivated)
        {
            pressurePlateSpriteRenderer.sprite = activatedPressurePlate;
        }
        else
        {
            pressurePlateSpriteRenderer.sprite = deactivatedPressurePlate;
        }
    }
}