using System.Collections.Generic;
using UnityEngine;

// Not used
public class AnalyticsLocationTimeSpend : MonoBehaviour
{
    private const int Delay = 10;

    [SerializeField] private PlayerCurrentLocation _playerLocation;

    private Analytics _analytics;
    private Location _currentLocation;
    private float _time = 0;

    private void OnEnable()
    {
        _analytics = Singleton<Analytics>.Instance;
        _playerLocation.EnteredLocation += OnEntered;
    }

    private void OnDisable()
    {
        _playerLocation.EnteredLocation -= OnEntered;
    }

    private void Start()
    {
        _currentLocation = _playerLocation.Location;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time < Delay)
            return;

        _time = 0f;
        AddTime(Delay);
    }

    private void OnEntered(Location location)
    {
        AddTime((int)_time);

        _time = 0f;
        _currentLocation = location;
    }

    private void AddTime(int value)
    {
        var key = SaveKey(_currentLocation);
        var saveTime = PlayerPrefs.GetInt(key, 0);
        var nextTime = saveTime + value;
        PlayerPrefs.SetInt(key, nextTime);

        if (saveTime == 0 || (saveTime / 60 == nextTime / 60))
            return;

        var minutes = nextTime / 60;
        if (minutes <= 5 || minutes % 10 == 0)
            _analytics.FireEvent(AppMetricaKey(_currentLocation), new Dictionary<string, object>() { { "ellapsed_time", minutes } });
    }

    private string AppMetricaKey(Location location) => $"location{(int)location}_time_spend";
    private string SaveKey(Location location) => $"Location{(int)location}_SaveKey";
}
