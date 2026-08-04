using System.Collections;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [Header("Door References")]
    [SerializeField] private GameObject successParticleEffect;
    [SerializeField] private GameObject lockObject;
    [SerializeField] private SpriteRenderer upperSpriteRenderer;
    [SerializeField] private SpriteRenderer lowerSpriteRenderer;
    [SerializeField] private Sprite upperDoorOpenSprite;
    [SerializeField] private Sprite lowerDoorOpenSprite;

    [Header("Door Settings")]
    [SerializeField] private int nextLevelIndex;
    [SerializeField] private Color doorColor = Color.red;

    private bool isOpen = false;
    private bool isTransitioning = false;

    private void Update()
    {
        // Stop checking once the door is already unlocked
        if (isOpen) return;

        CheckAllKeysCollected();
    }

    private void CheckAllKeysCollected()
    {
        Key[] keys = FindObjectsByType<Key>(FindObjectsSortMode.None);

        bool remainingKeyFound = false;
        foreach (Key key in keys)
        {
            // Match against the serialized doorColor instead of a hardcoded value
            if (key != null && key.KeyColor == doorColor)
            {
                remainingKeyFound = true;
                break; // A key still exists, no need to check the rest
            }
        }

        // Unlocks the door only when zero matching keys are found in the scene
        if (!remainingKeyFound)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        isOpen = true;

        if (upperSpriteRenderer != null && upperDoorOpenSprite != null)
            upperSpriteRenderer.sprite = upperDoorOpenSprite;

        if (lowerSpriteRenderer != null && lowerDoorOpenSprite != null)
            lowerSpriteRenderer.sprite = lowerDoorOpenSprite;

        if (lockObject != null)
        {
            Destroy(lockObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore trigger if door is still locked or level transition is already running
        if (!isOpen || isTransitioning) return;

        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TeleportSequenceCoroutine(collision.gameObject));
        }
    }

    private IEnumerator TeleportSequenceCoroutine(GameObject player)
    {
        isTransitioning = true;
        TeleportPlayer(player);

        yield return new WaitForSeconds(2f);

        if (LevelsManager.Instance != null)
        {
            LevelsManager.Instance.InstantiateLevel(nextLevelIndex);
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.gameObject.SetActive(false);

            if (successParticleEffect != null)
            {
                Instantiate(successParticleEffect, transform.position, Quaternion.identity);
            }
        }
    }
}