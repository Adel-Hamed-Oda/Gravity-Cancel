using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelDefinition", menuName = "Scriptable Objects/Level Definition", order = 1)]
public class LevelDefinition : ScriptableObject
{
    [Header("Level Defines")]
    [SerializeField] private int levelIndex = -1;
    public int LevelIndex { get { return levelIndex; } }

    [SerializeField] private string levelName = string.Empty;
    public string LevelName { get { return levelName; } }

    [SerializeField][TextArea] private string description = string.Empty;
    public string Description { get { return description; } }

    [SerializeField] private GameObject levelPrefab;
    public GameObject LevelPrefab { get { return levelPrefab; } }

    [SerializeField] private Vector3 playerSpawnPosition;
    public Vector3 PlayerSpawnPosition { get { return playerSpawnPosition; } }
}