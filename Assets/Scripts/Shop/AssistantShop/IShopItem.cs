using UnityEngine;

public interface IShopItem<T> where T : MonoBehaviour
{
    public Assistant Template { get; }
    public string Name { get; }
    public int Price { get; }
    public Sprite Icon { get; }
    public string GUID { get; }
}