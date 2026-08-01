using UnityEngine;

public class GravityManager : Manager<GravityManager>
{
    private Vector3 originalGravtiy;

    [HideInInspector] public bool IsGravityEnabled = true;

    protected override void Awake()
    {
        base.Awake();

        originalGravtiy = Physics.gravity;
        IsGravityEnabled = true;
    }

    public void EnableGravity()
    {
        Physics2D.gravity = originalGravtiy;
        IsGravityEnabled = true;
    }

    public void DisableGravity()
    {
        Physics2D.gravity = new Vector2(0, 0);
        IsGravityEnabled = false;
    }
}