using UnityEngine;

namespace BabyStack.Model
{
    public abstract class SavedObject<T> where T : class
    {
        private readonly string _guid;

        public SavedObject(string guid)
        {
            _guid = guid;
        }

        public void Save()
        {
            var jsonString = JsonUtility.ToJson(this as T);
            PlayerPrefs.SetString(_guid, jsonString);
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey(_guid) == false)
                return;

            var jsonString = PlayerPrefs.GetString(_guid);
            var loadedObject = JsonUtility.FromJson(jsonString, typeof(T));

            OnLoad(loadedObject as T);
        }

        protected abstract void OnLoad(T loadedObject);
    }
}