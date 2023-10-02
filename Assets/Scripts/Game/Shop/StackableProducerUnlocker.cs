using DG.Tweening;
using System.Collections;
using UnityEngine;

public class StackableProducerUnlocker : MonoBehaviour
{
    [SerializeField] private Stackable _stackable;
    [SerializeField] private UnlockRule _unlockRule;
    [SerializeField] private LevelCameraAnimation _openAnimation;
    [SerializeField] private bool _needShow = true;

    public StackableType Type => _stackable.Type;
    public bool Unlocked => _unlockRule.CanUnlock;

    private void Awake()
    {
        if (Unlocked)
            return;

        _unlockRule.AddUpdateListener(OnUpdate);
        gameObject.SetActive(false);
        transform.localScale = Vector3.zero;
    }

    private void OnDestroy()
    {
        if (Unlocked == false)
            _unlockRule.RemoveUpdateListener(OnUpdate);
    }

    private void OnUpdate()
    {
        if (_unlockRule.CanUnlock == false)
            return;

        _unlockRule.RemoveUpdateListener(OnUpdate);
        gameObject.SetActive(true);

        StartCoroutine(OpenCoroutine());
    }

    private IEnumerator OpenCoroutine()
    {
        if (_needShow)
            _openAnimation.LookTo(transform);

        yield return new WaitForSeconds(0.2f);

        transform.DOScale(1f, 1f);

        yield return new WaitForSeconds(1f);

        if (_needShow)
            _openAnimation.ResetLook();
    }
}
