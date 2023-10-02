using System;
using UnityEngine;

namespace BabyStack.Model
{
    [Serializable]
    public class MoneyBalance : SavedObject<MoneyBalance>
    {
        private const string SaveKey = "MoneyBalance";

        [SerializeField] private int _value;

        public MoneyBalance()
            : base(SaveKey)
        { }

        public event Action Changed;

        public int Value => _value;

        public void Add(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _value += value;
            Changed?.Invoke();
        }

        public void Spend(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _value -= value;

            if (_value < 0)
                _value = 0;

            Changed?.Invoke();
        }

        protected override void OnLoad(MoneyBalance loadedObject)
        {
            _value = loadedObject._value;
        }
    }
}