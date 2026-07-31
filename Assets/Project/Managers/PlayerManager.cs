using System.Collections;
using UnityEngine;

public class PlayerManager : Manager<PlayerManager>
{
    [SerializeField] private GameObject playerPrefab;

    public int maxPlayerHeight = 10;
    public int minPlayerHeight = -10;
    public float playerRespawnDelay = 3f;

    [Header("public variables that SHOULDN'T be changed")]
    public bool canSprint = false;
    public bool canDash = false;
    public bool canTeleportDash = false;
    public bool canGravityCancel = false;

    private GameObject playerInstance;

    private void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer(Vector2 position = default)
    {
        DestroyAllOtherInstances();

        playerInstance = Instantiate(playerPrefab, position, Quaternion.identity);
        playerInstance.GetComponent<PlayerController>().TeamColor = Color.green;
        playerInstance.GetComponent<PlayerController>().OnPlayerDeath += HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        DestroyAllOtherInstances();

        StartCoroutine(RespawnPlayer());
    }
    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(playerRespawnDelay);
        SpawnPlayer();
    }
    private void DestroyAllOtherInstances()
    {
        PlayerController[] existingPlayers = FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
        foreach (PlayerController player in existingPlayers)
        {
            Destroy(player.gameObject);
        }
    }
}