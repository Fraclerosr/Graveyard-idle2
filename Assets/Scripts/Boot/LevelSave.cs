using UnityEngine;

public class LevelSave : MonoBehaviour
{
    private const string CurrentLevelSaveKey = nameof(CurrentLevelSaveKey);

    private LevelLoader _levelLoader;

    private void OnEnable()
    {
        _levelLoader = Singleton<LevelLoader>.Instance;
        _levelLoader.Loaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        if (_levelLoader)
            _levelLoader.Loaded -= OnLevelLoaded;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        LevelLoader.Level currentLevel = (LevelLoader.Level)PlayerPrefs.GetInt(CurrentLevelSaveKey, 0);
        _levelLoader.LoadLevel(currentLevel, "Loading");
    }

    private void OnLevelLoaded(LevelLoader.Level level)
    {
        PlayerPrefs.SetInt(CurrentLevelSaveKey, (int)level);
    }
}
