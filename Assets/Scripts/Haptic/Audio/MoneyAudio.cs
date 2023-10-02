using System;
using System.Collections;
using UnityEngine;

public class MoneyAudio : BaseAudio
{
    [SerializeField] private float _delayBetweenMoneySound = 0.1f;
    [SerializeField] private SoundSettings _moneySound;
    [SerializeField] private SoundSettings _buySound;

    private bool _canPlayMoneySound = true;

    public void PlayMoneySound()
    {
        if (_canPlayMoneySound == false)
            return;

        _moneySound.MixerGroup.audioMixer.SetFloat("MoneyPitch", UnityEngine.Random.Range(0.98f, 1.03f));
        Play(_moneySound);

        _canPlayMoneySound = false;
        StartCoroutine(WaitBefore(_delayBetweenMoneySound, () => _canPlayMoneySound = true));
    }

    public void PlayBuySound()
    {
        Play(_buySound);
    }

    private IEnumerator WaitBefore(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}
