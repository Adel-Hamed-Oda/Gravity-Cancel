using System.Collections.Generic;
using UnityEngine;

public class GravityManager : Manager<GravityManager>
{
    [HideInInspector] public bool IsGravityEnabled = true;

    // We store the original gravity scale of each object so we can restore it accurately
    // (in case some objects originally had a gravity scale of 0.5, 2, etc.)
    private Dictionary<Rigidbody2D, float> originalGravityScales = new();

    protected override void Awake()
    {
        base.Awake();
        IsGravityEnabled = true;
    }

    public void DisableGravity()
    {
        if (!IsGravityEnabled) return;

        // Find all 2D Rigidbodies currently in the scene
        Rigidbody2D[] allRigidbodies = FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None);

        foreach (Rigidbody2D rb in allRigidbodies)
        {
            // Skip the player
            if (rb.CompareTag("Player")) continue;

            // Save the original scale if we haven't seen this object before
            if (!originalGravityScales.ContainsKey(rb))
            {
                originalGravityScales.Add(rb, rb.gravityScale);
            }

            // Disable gravity for this specific object
            rb.gravityScale = 0f;
        }

        IsGravityEnabled = false;
    }

    public void EnableGravity()
    {
        if (IsGravityEnabled) return;

        // Find all 2D Rigidbodies currently in the scene
        Rigidbody2D[] allRigidbodies = FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None);

        foreach (Rigidbody2D rb in allRigidbodies)
        {
            if (rb.CompareTag("Player")) continue;

            // Restore original gravity scale if we have it saved, otherwise default to 1
            if (originalGravityScales.ContainsKey(rb))
            {
                rb.gravityScale = originalGravityScales[rb];
            }
            else
            {
                rb.gravityScale = 1f;
            }
        }

        IsGravityEnabled = true;
    }
}