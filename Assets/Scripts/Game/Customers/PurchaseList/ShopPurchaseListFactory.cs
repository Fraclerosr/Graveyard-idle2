using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShopPurchaseListFactory : PurchaseListFactory
{
    [SerializeField] private List<UnlockableStackableItem> _items;
    [SerializeField] private int _maxCount = 3;

    public override PurchaseList Create() => 
        Create(Random.Range(1, _maxCount + 1));

    public override PurchaseList Create(int count)
    {
        var newList = new PurchaseList()
            .Add(FindUnlockedStackable(StackableType.Coffin).StackableType, count)
            .TryAdd(FindRandomUnlockedStackable(new List<StackableType> {StackableType.Bouquet, StackableType.UpgradedBouquet}), count)
            .TryAdd(FindRandomUnlockedStackable(new List<StackableType> {StackableType.Tombstone, StackableType.UpgradedTombstone}), count);

        return newList;
    }

    private UnlockableStackableItem FindRandomUnlockedStackable(List<StackableType> types)
    {
        IEnumerable<UnlockableStackableItem> items = new List<UnlockableStackableItem>();

        foreach (var stackableType in types)
            items = items.Concat(_items.Where(Unlocked(stackableType)));

        return !items.Any() ? null : items.ElementAt(Random.Range(0, items.Count()));
    }

    private UnlockableStackableItem FindUnlockedStackable(StackableType stackableType) =>
        _items.FirstOrDefault(Unlocked(stackableType));

    private static Func<UnlockableStackableItem, bool> Unlocked(StackableType stackableType) =>
        item => item.StackableType == stackableType && item.UnlockRule.CanUnlock;
}

[Serializable]
public class UnlockableStackableItem
{
    [field: SerializeField] public StackableType StackableType { get; private set; }
    [field: SerializeField] public UnlockRule UnlockRule { get; private set; }
}