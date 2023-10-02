using System.Collections;
using UnityEngine;

public class LocationTransition : UnlockableObject
{
    [SerializeField] private LevelLoader.Level _targetLevel;
    [SerializeField] private MoneyHolderTrigger _trigger;
    [SerializeField] private UnlockWorldSpaceButton _button;
    [SerializeField] private Animation _unlockAnimation;
    [SerializeField] private GameObject _newLocationSign;

    private void OnEnable()
    {
        _trigger.Enter += OnEntered;
        _trigger.Exit += OnExited;
        _button.Clicked += OnButtonClicked;
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnEntered;
        _trigger.Exit -= OnExited;
        _button.Clicked -= OnButtonClicked;
    }

    public override GameObject Unlock(Transform parent, bool onLoad, string guid)
    {
        gameObject.SetActive(true);
        
        if (!onLoad)
        {
            _unlockAnimation.Play();
            StartCoroutine(DisplayNewLocationSign());
        }

        return gameObject;
    }

    private IEnumerator DisplayNewLocationSign()
    {
        _newLocationSign.SetActive(true);
        yield return new WaitForSeconds(30f);
        _newLocationSign.SetActive(false);
    }

    private void OnEntered(MoneyHolder player)
    {
        _button.Enable();
    }

    private void OnExited(MoneyHolder player)
    {
        _button.Disable();
    }

    public void OnButtonClicked()
    {
        //Singleton<LevelLoader>.Instance.LoadLevel(_targetLevel);
    }
}