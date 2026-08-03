using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] private Entity entityComponent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Entity entity))
        {
            if (entity.TeamColor == entityComponent.TeamColor) return;

            // for now, I will add DIE() to the entity class later
            PlayerController playerController = collision.collider.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.Die();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Entity entity))
        {
            if (entity.TeamColor == entityComponent.TeamColor) return;

            // for now, I will add DIE() to the entity class later
            PlayerController playerController = collider.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.Die();
            }
        }
    }
}
