using System;
using UnityEngine;

public class Key : MonoBehaviour
{
    public event Action OnKeyCollected;

    [SerializeField] private GameObject keyParticleEffect;
    [SerializeField] private Color keyColor = Color.blue;
    public Color KeyColor { get { return keyColor; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CollectKey(collision.gameObject);
        }
    }
    private void CollectKey(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            OnKeyCollected?.Invoke();
            Instantiate(keyParticleEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}