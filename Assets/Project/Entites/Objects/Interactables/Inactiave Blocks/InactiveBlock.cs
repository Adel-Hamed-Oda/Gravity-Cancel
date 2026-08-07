using UnityEngine;

public class InactiveBlock : SignalReceiver
{
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private Collider2D Collider;
    [SerializeField] private Color originalColor = Color.white;
    [SerializeField] private Color inActiveColor = Color.gray;

    private bool state = false;

    private void Start()
    {
        if (CheckIfAllSignalsActive())
        {
            state = true;
            SpriteRenderer.color = originalColor;
            Collider.enabled = true;
        }
        else
        {
            state = false;
            SpriteRenderer.color = inActiveColor;
            Collider.enabled = false;
        }
    }

    private void Update()
    {
        if (state && !CheckIfAllSignalsActive())
        {
            state = false;
            SpriteRenderer.color = inActiveColor;
            Collider.enabled = false;

        }
        else if (!state && CheckIfAllSignalsActive())
        {
            state = true;
            SpriteRenderer.color = originalColor;
            Collider.enabled = true;
        }
    }
}
