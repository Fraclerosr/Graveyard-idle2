using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Analytics : Singleton<Analytics>
{
    private const string RegDayKey = "RegDay";
    private const string SessionCountKey = "SessionCount";
    private const string ReturningDay2Key = "ReturningDay2";

    private AnalyticsPlayTimeLogger _timeLogger;
    
    public event UnityAction EventSent;

    public TimeSpan AllPlayTime => _timeLogger.AllPlayTime;

    private string _regDay
    {
        get { return PlayerPrefs.GetString(RegDayKey, DateTime.Today.ToString()); }
        set { PlayerPrefs.SetString(RegDayKey, value); }
    }

    private int _sessionCount
    {
        get { return PlayerPrefs.GetInt(SessionCountKey, 0); }
        set { PlayerPrefs.SetInt(SessionCountKey, value); }
    }

    protected override void OnAwake()
    {
        _timeLogger = Singleton<AnalyticsPlayTimeLogger>.Instance;

        if (PlayerPrefs.HasKey(RegDayKey) == false)
        {
            DateTime regDay = DateTime.Today;
            _regDay = regDay.ToString();

            YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
            userProfile.Apply(YandexAppMetricaAttribute.CustomString("dd/MM/yy").WithValue(_regDay));
            AppMetrica.Instance.SetUserProfileID(new DuckyID().Value());
            AppMetrica.Instance.ReportUserProfile(userProfile);
        }

        _sessionCount += 1;

        SetBasicProperty(_sessionCount);
        FireEvent("game_start", new Dictionary<string, object>() { { "count", _sessionCount } });

        if (PlayerPrefs.HasKey(ReturningDay2Key) == false)
        {
            if (DateTime.Today.Subtract(DateTime.Parse(_regDay)).TotalHours >= 24)
            {
                FireAppsFlyerEvent("ret_1");
                PlayerPrefs.SetInt(ReturningDay2Key, 1);
            }
        }
    }

    public void LogTime(int elapsedMinutes)
    {
        var properties = new Dictionary<string, object>()
        {
            {"elapsed_minutes", elapsedMinutes}
        };

        FireEvent("play_time", properties);
    }

    public void OnSoftSpent(string productType, string productName, int amount)
    {
        var properties = new Dictionary<string, object>()
        {
            {"type", productType},
            {"name", productName},
            {"amount", amount}
        };

        FireEvent("soft_spent", properties);
    }

    public void FireEvent(string eventName, Dictionary<string, object> eventProps = null)
    {
        if (eventProps == null)
            eventProps = new Dictionary<string, object>();

        eventProps.Add("total_playtime_min", AllPlayTime.Minutes);
        eventProps.Add("total_playtime_sec", AllPlayTime.Minutes * 60 + AllPlayTime.Seconds);

        AppMetrica.Instance.ReportEvent(eventName, eventProps);

        EventSent?.Invoke();
    }

    public void FireAppsFlyerEvent(string eventName, Dictionary<string, string> eventValues = null)
    {
        
    }

    private void SetBasicProperty(int sessionCount)
    {
        int daysInGame = DateTime.Today.Subtract(DateTime.Parse(_regDay)).Days;

        YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        userProfile.Apply(YandexAppMetricaAttribute.CustomCounter("days_in_game").WithDelta(daysInGame));
        userProfile.Apply(YandexAppMetricaAttribute.CustomCounter("session_count").WithDelta(sessionCount));

        AppMetrica.Instance.SetUserProfileID(new DuckyID().Value());
        AppMetrica.Instance.ReportUserProfile(userProfile);
    }
}
