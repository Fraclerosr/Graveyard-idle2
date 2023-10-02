using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AssistantShopData", menuName = "AssistantShop/AssistantShopData", order = 51)]
public class AssistantShopData : ScriptableObject
{
    [SerializeField] private string _saveKey;
    [SerializeField] private List<AssistantData> _data = new List<AssistantData>();

    public string SaveKey => _saveKey;
    public IEnumerable<AssistantData> Data => _data;
}
