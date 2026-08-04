using UnityEngine;

public class PressurePlate : SignalProvider
{
    [Header("References")]
    [SerializeField] private SpriteRenderer pressurePlateSpriteRenderer;
    [SerializeField] private Sprite activatedPressurePlate;
    [SerializeField] private Sprite deactivatedPressurePlate;

    private bool isActivated = false;
    private int objectsOnPlate = 0;

    private void Start()
    {
        UpdatePressurePlateSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectsOnPlate++;

        if (objectsOnPlate > 0 && !isActivated)
        {
            isActivated = true;
            Activate();
            UpdatePressurePlateSprite();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectsOnPlate--;

        if (objectsOnPlate < 0)
        {
            objectsOnPlate = 0;
        }

        if (objectsOnPlate == 0 && isActivated)
        {
            isActivated = false;
            Deactivate();
            UpdatePressurePlateSprite();
        }
    }

    private void UpdatePressurePlateSprite()
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