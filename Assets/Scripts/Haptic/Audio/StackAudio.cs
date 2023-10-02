using System.Collections;
using UnityEngine;

public class StackAudio : BaseAudio
{
    [SerializeField] private float _delayBeforeResetPitch = 2f;
    [SerializeField] private float _maxAddToStackPitch = 1.5f;
    [Space, SerializeField] private SoundSettings _addToStackSound;
    [SerializeField] private SoundSettings _removeFromStackSound;

    private float _addToStackPitch = 1f;
    private Coroutine _resetPitchCoroutine;

    public void PlayAddToStackSound()
    {
        if (_resetPitchCoroutine != null)
        {
            StopCoroutine(_resetPitchCoroutine);
            _addToStackPitch += _addToStackPitch < _maxAddToStackPitch ? 0.05f : 0;
        }    

        _addToStackSound.MixerGroup.audioMixer.SetFloat("AddToStackPitch", _addToStackPitch);
        Play(_addToStackSound);

        _resetPitchCoroutine = StartCoroutine(ResetPitch(_delayBeforeResetPitch));
    }

    public void PlayRemoveFromStackSound()
    {
        _removeFromStackSound.MixerGroup.audioMixer.SetFloat("RemoveFromStackPitch", UnityEngine.Random.Range(0.95f, 1.06f));
        Play(_removeFromStackSound);
    }

    private IEnumerator ResetPitch(float delay)
    {
        yield return new WaitForSeconds(delay);

        _addToStackPitch = 1f;
    }
}
