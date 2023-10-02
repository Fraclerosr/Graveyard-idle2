using System;
using UnityEngine;

namespace BabyStack.Model
{
    [Serializable]
    public class DynamicCost
    {
        [SerializeField] private int _totalCost;
        [SerializeField] private int _currentCost;

        public DynamicCost(int totalCost)
        {
            if (totalCost < 1)
                throw new ArgumentOutOfRangeException(nameof(totalCost));

            _totalCost = totalCost;
            _currentCost = totalCost;
        }

        public int TotalCost => _totalCost;
        public int CurrentCost => _currentCost;

        public void Subtract(int value)
        {
            _currentCost -= value;

            if (_currentCost < 0)
                _currentCost = 0;
        }
    }
}