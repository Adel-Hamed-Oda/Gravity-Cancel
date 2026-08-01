using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelsManager : Manager<LevelsManager>
{
    [SerializeField] private Image transitionImage;

    [Header("Levels")]
    [SerializeField] private LevelDefinition[] levelDefinitions;

    private GameObject currentLevel;

    private void Start()
    {
        transitionImage.color = new Color(0, 0, 0, 0);

        if (levelDefinitions.Length == 0)
        {
            Debug.LogWarning("No level definitions found. Please ensure that the level definitions are assigned in the LevelsManager.");
            return;
        }
        _InstantiateLevel(-1);
    }

    private void _InstantiateLevel(int index)
    {
        foreach (LevelDefinition definition in levelDefinitions)
        {
            if (definition.LevelIndex == index)
            {
                if (currentLevel != null)
                {
                    DestroyImmediate(currentLevel);
                }
                currentLevel = Instantiate(definition.LevelPrefab);

                PlayerManager playerManager = currentLevel.GetComponent<PlayerManager>();
                if (playerManager == null)
                {
                    Debug.LogWarning("No player manager found in the level prefab. Please ensure that the level prefab has a PlayerManager component.");
                    return;
                }

                playerManager.SpawnPlayer(definition.PlayerSpawnPosition);

                break;
            }
        }

        Debug.LogWarning($"Level with index {index} not found. Please ensure that the level index is correct and that the level definition exists.");
    }

    public void InstantiateLevel(int index)
    {
        StartCoroutine(InstantiateLevelCoroutine(index));
    }
    private IEnumerator InstantiateLevelCoroutine(int index)
    {
        while (transitionImage.color.a < 1)
        {
            transitionImage.color = new Color(0, 0, 0, transitionImage.color.a + Time.deltaTime);
            yield return null;
        }
        transitionImage.color = new Color(0, 0, 0, 1);

        Destroy(FindFirstObjectByType<PlayerController>().gameObject);
        _InstantiateLevel(index);

        yield return new WaitForSeconds(1f);
        while (transitionImage.color.a > 0)
        {
            transitionImage.color = new Color(0, 0, 0, transitionImage.color.a - Time.deltaTime);
            yield return null;
        }
        transitionImage.color = new Color(0, 0, 0, 0);
    }
}