using System.Collections;
using UnityEngine;

public class PlayerManager : Manager<PlayerManager>
{
    [SerializeField] private GameObject playerPrefab;

    public int maxPlayerHeight;
    public int minPlayerHeight;
    public float playerRespawnDelay = 3f;

    public bool canSprint = false;
    public bool canDash = false;
    public bool canTeleportDash = false;
    public bool canGravityCancel = false;

    private GameObject playerInstance;

    public void SpawnPlayer(Vector2 position = default)
    {
        DestroyAllOtherInstances();

        playerInstance = Instantiate(playerPrefab, position, Quaternion.identity);
        playerInstance.GetComponent<PlayerController>().TeamColor = Color.green;
        playerInstance.GetComponent<PlayerController>().OnPlayerDeath += HandlePlayerDeath;

        playerInstance.GetComponentInChildren<SprintController>().enabled = canSprint;
        playerInstance.GetComponentInChildren<DashController>().enabled = canDash;
        playerInstance.GetComponentInChildren<TeleportDashController>().enabled = canTeleportDash;
        playerInstance.GetComponentInChildren<GravityCancel>().enabled = canGravityCancel;
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