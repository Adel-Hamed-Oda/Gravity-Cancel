using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int NextLevelIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FreezePlayer(collision.gameObject);

            LevelsManager.Instance.InstantiateLevel(NextLevelIndex);
        }
    }

    private void FreezePlayer(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
            Rigidbody2D rb = playerController.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }
}