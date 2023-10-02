using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelLoader : Singleton<LevelLoader>
{
    private const string ScreenTemplatePath = "Boot/LoadingSceneScreen";

    private Dictionary<Level, string> _levels = new Dictionary<Level, string>()
    {
        //{LevelLoader.Level.HospitalManger, "Level1" },
        {LevelLoader.Level.SchoolBar, "ScoolStack" },
    };

    public event UnityAction<Level> Loaded;

    public void LoadLevel(Level level, string loadingText = "Loading")
    {
        var screenReference = Resources.Load<LevelLoadingScreen>(ScreenTemplatePath);
        var loadingScreen = Instantiate(screenReference);

        loadingScreen.LoadScene(_levels[level], loadingText, () =>
        {
            Loaded?.Invoke(level);
            Destroy(loadingScreen.gameObject);
        });
    }

    public enum Level
    {
        //HospitalManger,
        SchoolBar,
    }
}
