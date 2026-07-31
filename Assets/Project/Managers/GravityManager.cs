using UnityEngine;

public class GravityManager : Manager<GravityManager>
{
    private Vector3 originalGravtiy;

    public bool IsGravityEnabled => Physics.gravity == originalGravtiy;

    protected override void Awake()
    {
        base.Awake();

        originalGravtiy = Physics.gravity;
    }

    public void EnableGravity()
    {
        Physics2D.gravity = originalGravtiy;
    }

    public void DisableGravity()
    {
        Physics2D.gravity = new Vector2(0, 0);
    }
}