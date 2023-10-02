using UnityEngine;

public class AnalyticsMoneyProperty : MonoBehaviour
{
    [SerializeField] private MoneyHolder _moneyHolder;

    private Analytics _analytics;

    private void Awake()
    {
        _analytics = Singleton<Analytics>.Instance;
    }

    private void OnEnable()
    {
        _analytics.EventSent += OnSentEvent;
    }

    private void OnDisable()
    {
        _analytics.EventSent -= OnSentEvent;
    }

    private void OnSentEvent()
    {
        YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        userProfile.Apply(YandexAppMetricaAttribute.CustomNumber("current_soft").WithValue(_moneyHolder.Value));

        AppMetrica.Instance.SetUserProfileID(new DuckyID().Value());
        AppMetrica.Instance.ReportUserProfile(userProfile);
    }
}
