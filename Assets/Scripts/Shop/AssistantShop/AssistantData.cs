using UnityEngine;
using System;

[Serializable]
public class AssistantData : IShopItem<Assistant>
{
    [SerializeField] private Assistant _template;
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _guid;
    [SerializeField] private bool _alwaysUnlocked;

    public Assistant Template => _template;
    public string Name => _name;
    public int Price => _price;
    public Sprite Icon => _icon;
    public string GUID => _guid;
    public bool AlwaysUnlocked => _alwaysUnlocked;
}