using UnityEngine;
using BabyStack.Settings;

public class SetupSettingPresenters : MonoBehaviour
{
    [SerializeField] private BackgroundMusic _music;

    [Header("Presenters")]
    [SerializeField] private SettingPresenter _audioPresenter;
    [SerializeField] private SettingPresenter _musicPresenter;
    [SerializeField] private SettingPresenter _vibrationsPresenter;

    private void Start()
    {
        _audioPresenter.Init(new Audio());
        _musicPresenter.Init(_music);
        _vibrationsPresenter.Init(new Vibration());
    }
}
