using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class UnlockableReference : UnlockableObject
{
    [SerializeField] private ParticleSystem _unlockEffect;
    [SerializeField] private MonoBehaviour _template;

    public event UnityAction<MonoBehaviour, bool, string> Unlocked;

    public override GameObject Unlock(Transform parent, bool onLoad, string guid)
    {
        var inst = Instantiate(_template, parent);
        
        Vector3 startScale = inst.transform.localScale;
        inst.transform.localScale = Vector3.zero;
        inst.transform.DOScale(startScale, 1f);

        if (onLoad == false)
            Instantiate(_unlockEffect, inst.transform.position + Vector3.up, _unlockEffect.transform.rotation);

        Unlocked?.Invoke(inst, onLoad, guid);

        return inst.gameObject;
    }
}
