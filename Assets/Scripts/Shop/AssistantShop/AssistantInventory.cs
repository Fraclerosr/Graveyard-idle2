using System.Collections.Generic;
using BabyStack.Model;
using System.Linq;
using System;
using UnityEngine;

[Serializable]
public class AssistantInventory : SavedObject<AssistantInventory>, IEquatable<AssistantInventory>
{
    [SerializeField] private List<string> _unlockedGuid = new List<string>();

    private AssistantShopData _shopData;

    public AssistantInventory(AssistantShopData shopData)
        : base(shopData.SaveKey)
    {
        _shopData = shopData;
    }

    public static event Action<AssistantInventory> Added;

    public AssistantShopData ShopData => _shopData;
    public int Count => _unlockedGuid.Count;
    public IEnumerable<AssistantData> Data => from data in _shopData.Data
                                              where _unlockedGuid.Contains(data.GUID)
                                              select data;

    public void Add(AssistantData data)
    {
        _unlockedGuid.Add(data.GUID);
        Added?.Invoke(this);
    }

    public bool Contains(AssistantData data)
    {
        return _unlockedGuid.Contains(data.GUID);
    }

    protected override void OnLoad(AssistantInventory loadedObject)
    {
        _unlockedGuid = loadedObject._unlockedGuid;
    }

    public bool Equals(AssistantInventory other)
    {
        return other.ShopData.SaveKey == ShopData.SaveKey;
    }
}
