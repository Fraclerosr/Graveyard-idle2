using System;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Linq;
using BabyStack.Model;
using Object = UnityEngine.Object;

public class Conveyor : LockedGameObject, IModificationListener<float>
{ 
    [SerializeField] private StackableTransformation _stackableTransformation;
    [SerializeField] private StackableType _resourceType;
    [SerializeField] private StackPresenter _startStack;
    [SerializeField] private StackPresenter _endStack;
    [SerializeField] private Transform _transfomrateBox;
    [SerializeField] private ParticleSystem _transformateEffect;
    [SerializeField] private TimerView _timerView;
    [SerializeField] private Transform _takePoint;
    [SerializeField] private Transform _placePoint;

    private readonly Timer _timer = new Timer();
    private float _childProcessDuration => 2.5f / _speedRate;
    private Stackable _removedItem;
    private float _speedRate = 1;

    public event Action TransformedItem;
    
    public int EndCount => _endStack.Count;
    public StackableType ProducibleType => _stackableTransformation.Type;
    public StackableType ResourceType => _resourceType;

    public Transform TakePoint => _takePoint;
    public Transform PlacePoint => _placePoint;
    public bool FullInput => _startStack.IsFull;


    private void Start() => 
        StartCoroutine(TransformateProcess());

    private void OnEnable()
    {
        _timerView.Init(_timer);
        _timer.Completed += OnTimeOver;
    }

    private void Update() => 
        _timer.Tick(Time.deltaTime);

    private void OnDisable() => 
        _timer.Completed -= OnTimeOver;

    private IEnumerator TransformateProcess()
    {
        while (true)
        {
            yield return new WaitUntil(() => _endStack.IsFull == false && _startStack.Count > 0 && !_removedItem);

            _removedItem = _startStack.RemoveFromStack(_startStack.Data.First().Type);

            _removedItem.transform.DOComplete(true);
            _removedItem.transform.DOMove(_transfomrateBox.position, 1f);

            _transfomrateBox.DOShakeScale(_childProcessDuration, 20f);
            
            _timer.Start(_childProcessDuration);
        }
    }

    private void OnTimeOver()
    {
        if(!_removedItem)
            return;
        
        _transformateEffect.Play();
        Destroy(_removedItem.gameObject);
        var producedItem = Instantiate(_stackableTransformation.Transform(_removedItem));
        producedItem.transform.position = _transfomrateBox.position;
        _endStack.AddToStack(producedItem);
        _removedItem = null;
        TransformedItem?.Invoke();
    }

    public void OnModificationUpdate(float value) => 
        _speedRate = value;

    public bool CanAddToStack(StackableType item) => 
        _startStack.CanAddToStack(item);
}