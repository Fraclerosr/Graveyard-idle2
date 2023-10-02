using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class LocationItemsContainer : MonoBehaviour
{
    [SerializeField] private Location _location;
    [SerializeField] private StackPresenter _stackPresenter;
    [SerializeField] private StackableTypes _stackableTypes;

    public Location Location => _location;
    public bool CanRemoveItem => _stackPresenter.Count > 0;
    public StackableType NextItemType => _stackableTypes.Value.First(type => _stackPresenter.CanRemoveFromStack(type));

    public void Add(IEnumerable<Stackable> items)
    {
        foreach (var item in items)
        {
            if (_stackableTypes.Contains(item.Type))
                _stackPresenter.AddToStack(item);
            else
                Destroy(item.gameObject);
        }
    }

    public Stackable RemoveItem()
    {
        if (CanRemoveItem == false)
            throw new InvalidOperationException();

        return _stackPresenter.RemoveFromStack(NextItemType);
    }
}
