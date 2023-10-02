using BabyStack.Model;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class BuyZonePresenter : GUIDObject
{
    [Space(10)]
    [SerializeField] private int _totalCost;
    [SerializeField] private MoneyHolderTrigger _trigger;
    [SerializeField] private BuyZoneView _view;
    [SerializeField] private UnlockableObject _unlockable;

    private BuyZone _buyZone;
    private Coroutine _tryBuy;

    public event UnityAction<BuyZonePresenter> FirstTimeUnlocked;
    public event UnityAction<BuyZonePresenter> Unlocked;

    public abstract event UnityAction Unlocking;

    public int TotalCost => _totalCost;
    public UnlockableObject UnlockableObject => _unlockable;


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_view)
            _view.RenderCost(_totalCost);
    }
#endif

    private void Awake()
    {
        _buyZone = BuyZone.GetZone(_totalCost, GUID);
    }

    private void OnEnable()
    {
        _trigger.Enter += OnPlayerTriggerEnter;
        _trigger.Exit += OnPlayerTriggerExit;
        _buyZone.Unlocked += OnBuyZoneUnlocked;
        _buyZone.CostUpdated += OnCostUpdated;

        OnEnabled();
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnPlayerTriggerEnter;
        _trigger.Exit -= OnPlayerTriggerExit;
        _buyZone.Unlocked -= OnBuyZoneUnlocked;
        _buyZone.CostUpdated -= OnCostUpdated;

        OnDisabled();
    }

    private void Start()
    {
        _buyZone.Load();
        UpdateCost();

        OnBuyZoneLoaded(_buyZone);
    }

    public void PlayNewText()
    {
        _view.PlayNewText();
    }

    private void OnPlayerTriggerEnter(MoneyHolder moneyHolder)
    {
        var movement = moneyHolder.GetComponent<PlayerMovement>();

        if (_tryBuy != null)
            StopCoroutine(_tryBuy);

        _tryBuy = StartCoroutine(TryBuy(moneyHolder, movement));
        
        OnEnter();
    }

    private void OnPlayerTriggerExit(MoneyHolder moneyHolder)
    {
        StopCoroutine(_tryBuy);
        _buyZone.Save();

        OnExit();
    }

    private void OnBuyZoneUnlocked(bool onLoad)
    {
        _trigger.Disable();
        _view.Hide();
        _unlockable.Unlock(transform, onLoad, GUID);

        Unlocked?.Invoke(this);

        if (onLoad == false)
            FirstTimeUnlocked?.Invoke(this);
    }

    private IEnumerator TryBuy(MoneyHolder moneyHolder, PlayerMovement playerMovement)
    {
        yield return null;

        bool delayed = false;
        while (true)
        {
            if (playerMovement.IsMoving == false)
            {
                if (delayed == false)
                    yield return new WaitForSeconds(0.75f);

                BuyFrame(_buyZone, moneyHolder);
                UpdateCost();
                delayed = true;
            }
            else
            {
                delayed = false;
            }

            yield return null;
        }
    }

    private void OnCostUpdated(int value)
    {
        UpdateCost();
    }

    private void UpdateCost()
    {
        _view.RenderCost(_buyZone.CurrentCost);
    }

    protected virtual void OnBuyZoneLoaded(BuyZone buyZone) { }
    protected virtual void OnEnabled() { }
    protected virtual void OnDisabled() { }
    protected virtual void OnEnter() { }
    protected virtual void OnExit() { }
    protected abstract void BuyFrame(BuyZone buyZone, MoneyHolder moneyHolder);
}
