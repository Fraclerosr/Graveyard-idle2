using UnityEngine;

public class StackHaptics : BaseHaptics
{
    [SerializeField] private PlayerStackPresenter _playerStackPresenter;
    [SerializeField] private StackAudio _stackAudio;

    private void OnEnable()
    {
        _playerStackPresenter.Added += OnAdded;
        _playerStackPresenter.Removed += OnRemoved;
    }

    private void OnDisable()
    {
        _playerStackPresenter.Added -= OnAdded;
        _playerStackPresenter.Removed -= OnRemoved;
    }

    private void OnAdded(Stackable stackable)
    {
        Vibrate();
        _stackAudio.PlayAddToStackSound();
    }

    private void OnRemoved(Stackable stackable)
    {
        Vibrate();
        _stackAudio.PlayRemoveFromStackSound();
    }
}
