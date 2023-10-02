using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Events;

namespace BabyStack.Model
{
    public class StackStorage
    {
        private readonly List<Stackable> _stackables = new List<Stackable>();
        private readonly List<StackableTypes> _allTypesThatCanBeAdded;
        private StackableTypes _currentTypesThatCanBeAdded;
        private int _capacity;

        public event UnityAction CapacityChanged;

        public int Count => _stackables.Count;
        public int Capacity => _capacity;
        public IEnumerable<Stackable> Data => _stackables;

        public StackStorage(int capacity, List<StackableTypes> allTypesThatCanBeAdded)
        {
            _capacity = capacity;
            _allTypesThatCanBeAdded = allTypesThatCanBeAdded;
        }

        public event Action<Stackable> Added;
        public event Action<Stackable> Removed;

        public bool CanAdd(StackableType stackableType)
        {
            if (_stackables.Count == 0)
                return FindTypesThatCanBeAdded(stackableType) != null;

            if (_stackables.Count == _capacity)
                return false;

            return _currentTypesThatCanBeAdded.Contains(stackableType);
        }

        public void Add(Stackable stackable)
        {
            if (CanAdd(stackable.Type) == false)
                throw new InvalidOperationException(nameof(stackable) + " can't be added");

            if (_currentTypesThatCanBeAdded == null)
                _currentTypesThatCanBeAdded = FindTypesThatCanBeAdded(stackable.Type);

            _stackables.Add(stackable);
            Added?.Invoke(stackable);
        }

        public void Remove(Stackable stackable)
        {
            if (_stackables.Contains(stackable) == false)
                throw new InvalidOperationException(nameof(stackable) + " not in stack");

            _stackables.Remove(stackable);

            if (_stackables.Count == 0)
                _currentTypesThatCanBeAdded = null;

            Removed?.Invoke(stackable);
        }

        public void Clear()
        {
            _stackables.Clear();
        }

        public bool Contains(StackableType stackableType)
        {
            if (_stackables.Count == 0)
                return false;

            foreach (var stackable in _stackables)
                if (stackable.Type == stackableType)
                    return true;

            return false;
        }

        public int CalculateCount(StackableType stackableType)
        {
            return _stackables.Count(stackable => stackable.Type == stackableType);
        }

        public Stackable FindLast(StackableType stackableType)
        {
            if (Contains(stackableType) == false)
                throw new InvalidOperationException(nameof(stackableType) + " not in stack");

            return _stackables.FindLast(stackable => stackable.Type == stackableType);
        }

        public void ChangeCapacity(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            _capacity = capacity;
            CapacityChanged?.Invoke();
        }

        private StackableTypes FindTypesThatCanBeAdded(StackableType stackableType)
        {
            return _allTypesThatCanBeAdded.Find(types => types.Contains(stackableType));
        }
    }
}