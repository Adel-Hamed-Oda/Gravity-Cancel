using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int nextLevelIndex;
    [SerializeField] private GameObject successParticleEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TeleportSequenceCoroutine(collision.gameObject));
        }
    }

    private IEnumerator TeleportSequenceCoroutine(GameObject player)
    {
        TeleportPlayer(player);
        yield return new WaitForSeconds(2f);
        LevelsManager.Instance.InstantiateLevel(nextLevelIndex);
    }
    private void TeleportPlayer(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.gameObject.SetActive(false);
            Instantiate(successParticleEffect, transform);
        }
    }
}