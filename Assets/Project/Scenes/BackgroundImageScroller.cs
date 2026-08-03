using UnityEngine;
using UnityEngine.UI;

public class BackgroundImageScroller : MonoBehaviour
{
    [SerializeField] private LevelsManager levelsManager;
    [SerializeField] private Transform trackedTransform;

    [Header("Background Layer 1")]
    [SerializeField] private Image backgroundLayer1;
    [SerializeField] private Vector2 backgroundLayer1ScrollRatio = new(0.1f, 0f);

    [Header("Background Layer 2")]
    [SerializeField] private Image backgroundLayer2;
    [SerializeField] private Vector2 backgroundLayer2ScrollRatio = new(0.2f, 0f);

    private void Start()
    {
        InitializeBackgrounds(null);
        levelsManager.OnLevelInstantiated += InitializeBackgrounds;
    }

    private void InitializeBackgrounds(LevelDefinition definition)
    {
        if (definition != null)
        {
            backgroundLayer1.sprite = definition.Background1;
            backgroundLayer2.sprite = definition.Background2;
        }

        if (backgroundLayer1.sprite == null) backgroundLayer1.color = new Color(1, 1, 1, 0);
        else backgroundLayer1.color = Color.white;

        if (backgroundLayer2.sprite == null) backgroundLayer2.color = new Color(1, 1, 1, 0);
        else backgroundLayer2.color = Color.white;
    }

    private void Update()
    {
        ScrollBackgrounds();
    }

    private void ScrollBackgrounds()
    {
        Vector2 playerPos = trackedTransform.position;

        if (backgroundLayer1 != null)
        {
            backgroundLayer1.material.mainTextureOffset = playerPos * backgroundLayer1ScrollRatio;
        }

        if (backgroundLayer2 != null)
        {
            backgroundLayer2.material.mainTextureOffset = playerPos * backgroundLayer2ScrollRatio;
        }
    }
}