using UnityEngine;
using System;

namespace BabyStack.Model
{
    [Serializable]
    public struct ModificationData<T>
    {
        [SerializeField] private int _price;
        [SerializeField] private T _value;

        public ModificationData(int price, T value)
        {
            _price = price;
            _value = value;
        }

        public int Price => _price;
        public T Value => _value;
    }
}