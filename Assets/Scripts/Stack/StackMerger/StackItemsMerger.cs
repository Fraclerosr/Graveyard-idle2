using System.Linq;
using UnityEngine;

public class StackItemsMerger : MonoBehaviour
{
    [SerializeField] private StackPresenter _stackPresenter;
    [SerializeField] private Stackable _mergedItemTemplate;
    [SerializeField] private StackableType _firstType;
    [SerializeField] private StackableType _secondType;

    private void OnEnable() => 
        _stackPresenter.Added += OnAdded;

    private void OnDisable() => 
        _stackPresenter.Added -= OnAdded;

    private void OnAdded(Stackable stackable)
    {
        if (CanRemoveFromStack(_firstType) && CanRemoveFromStack(_secondType))
        {
            Stackable firstItem = Destroy(_firstType);
            Stackable secondItem = Destroy(_secondType);

            Vector3 position = firstItem.transform.position;
                
            if (firstItem.transform.position.y < secondItem.transform.position.y)
                position = secondItem.transform.position;

            Stackable newItem = Instantiate(_mergedItemTemplate, position , firstItem.transform.rotation);

            OnMerged(firstItem, secondItem, newItem);
            
            if (_stackPresenter.CanAddToStack(newItem.Type))
                _stackPresenter.AddToStack(newItem);
        }
    }

    protected virtual void OnMerged(Stackable firstItem, Stackable secondItem, Stackable newItem)
    {
    }

    private Stackable Destroy(StackableType stackableType)
    {
        Stackable fromStack = _stackPresenter.RemoveFromStack(stackableType);
        Transform destroying = fromStack.gameObject.transform;
        Destroy(destroying.gameObject);

        return fromStack;
    }

    private bool CanRemoveFromStack(StackableType stackableType) => 
        _stackPresenter.CanRemoveFromStack(stackableType);
}