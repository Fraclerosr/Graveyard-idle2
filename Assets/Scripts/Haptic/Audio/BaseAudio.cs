using System;
using UnityEngine;
using UnityEngine.Audio;
using BabyStack.Settings;

public abstract class BaseAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    
    private Audio _audio;

    private void Awake()
    {
        _audio = new Audio();
    }

    protected void Play(SoundSettings soundSettings)
    {
        if (_audio.IsEnable == false)
            return;

        _audioSource.outputAudioMixerGroup = soundSettings.MixerGroup;
        _audioSource.PlayOneShot(soundSettings.Clip);
    }
}

[Serializable]
public class SoundSettings
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioMixerGroup _mixerGroup;

    public AudioClip Clip => _clip;
    public AudioMixerGroup MixerGroup => _mixerGroup;
}
