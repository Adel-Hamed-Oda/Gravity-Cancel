using System.Collections;
using UnityEngine;

public class GravityCancel : MonoBehaviour
{
    [SerializeField] private GameObject gravityCancelExplosion;
    [SerializeField] private GameObject gravityReturnImplosion;

    [Header("Gravity Cancel Settings")]
    [SerializeField] private float gravityCancelTime;
    [SerializeField] private float gravityCancelCooldown;

    private float gravityCancelCooldownTimer;
    private float maxUpSpeedBeforeCancel;
    private float maxDownSpeedBeforeCancel;

    private void Update()
    {
        gravityCancelCooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q) && GravityManager.Instance.IsGravityEnabled && gravityCancelCooldownTimer <= 0)
        {
            CancelGravity();
        }
    }

    private void CancelGravity()
    {
        GravityManager.Instance.DisableGravity();
        gravityCancelCooldownTimer = 9999; // IDK why but it's funny

        Instantiate(gravityCancelExplosion, transform);

        StartCoroutine(ReturnGravity());
    }
    private IEnumerator ReturnGravity()
    {
        yield return new WaitForSeconds(gravityCancelTime - 1f);

        Instantiate(gravityReturnImplosion, transform);

        yield return new WaitForSeconds(1f);

        gravityCancelCooldownTimer = gravityCancelCooldown;
        GravityManager.Instance.EnableGravity();
    }
}