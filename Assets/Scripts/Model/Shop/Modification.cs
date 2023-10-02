using System;
using System.Collections.Generic;
using UnityEngine;

namespace BabyStack.Model
{
    [Serializable]
    public abstract class Modification<T> : IModification<T>
    {
        [SerializeField] private int _currentModification;

        private string _guid;

        public Modification(string guid)
        {
            _guid = guid;
            _currentModification = 0;
        }

        public int CurrentModificationLevel => _currentModification;
        public T CurrentModificationValue => Data[_currentModification].Value;
        public abstract List<ModificationData<T>> Data { get; }

        public bool TryGetNextModification(out ModificationData<T> modificationData)
        {
            modificationData = default;

            if (_currentModification >= Data.Count - 1)
                return false;

            modificationData = Data[_currentModification + 1];
            return true;
        }

        public void Upgrade()
        {
            if (_currentModification >= Data.Count - 1)
                throw new InvalidOperationException();

            _currentModification++;
        }

        public void Save()
        {
            PlayerPrefs.SetInt(_guid, _currentModification);
        }


        public void Load()
        {
            if (PlayerPrefs.HasKey(_guid) == false)
                return;

            var savedModification = PlayerPrefs.GetInt(_guid);
            _currentModification = savedModification;
        }
    }
}