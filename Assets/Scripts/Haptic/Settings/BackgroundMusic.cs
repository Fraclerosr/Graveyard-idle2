using UnityEngine;
using BabyStack.Settings;

public class BackgroundMusic : MonoBehaviour, ISetting
{
    [SerializeField] private AudioSource _audioSource;

    private Music _music;

    public bool IsEnable => _music.IsEnable;

    private void Awake()
    {
        _music = new Music();

        if (_music.IsEnable)
            _audioSource.Play();
    }

    public void Enable()
    {
        _music.Enable();
        _audioSource.Play();
    }

    public void Disable()
    {
        _music.Disable();
        _audioSource.Stop();
    }
}
