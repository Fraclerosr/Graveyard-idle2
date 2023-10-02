using System;
using System.Collections.Generic;

[Serializable]
public class PurchaseList
{
    private readonly Dictionary<StackableType, int> _list = new Dictionary<StackableType, int>();

    public event Action BecameEmpty;

    public IReadOnlyDictionary<StackableType, int> Items => _list;
    public int TotalPrice { get; private set; }
    public bool Empty => _list.Count == 0;

    public PurchaseList Add(StackableType type, int count)
    {
        if(_list.ContainsKey(type))
            _list[type] += count;
        else
            _list.Add(type, count);

        TotalPrice += count * 10;
        
        return this;
    }

    public void Remove(StackableType stackableType)
    {
        if (_list.ContainsKey(stackableType) == false)
            throw new InvalidOperationException();

        if (_list[stackableType] > 1)
            _list[stackableType]--;
        else
            _list.Remove(stackableType);

        if (_list.Count == 0)
            BecameEmpty?.Invoke();
    }
}