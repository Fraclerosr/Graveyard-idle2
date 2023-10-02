using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ListProgress", menuName = "Progress/ListProgress", order = 51)]
public class ListProgress : Progress
{
    private ListProgressData _data;

    public override event UnityAction Updated;

    public IEnumerable<string> Data => _data.Data;
    public override int CurrentProgress => _data.Progress;

    public bool Contains(string guid) => _data.Contains(guid);

    public void Add(string guid)
    {
        _data.Add(guid);
        Updated?.Invoke();
    }

    public override void Load()
    {
        _data = ListProgressData.Load(SaveKey);
    }

    public override void Save()
    {
        _data.Save(SaveKey);
    }

    [Serializable]
    private class ListProgressData
    {
        [SerializeField] private List<string> _guidList;

        public ListProgressData()
        {
            _guidList = new List<string>();
        }

        public IEnumerable<string> Data => _guidList;
        public int Progress => _guidList.Count;
        public bool Contains(string guid) => _guidList.Contains(guid);

        public void Add(string guid)
        {
            if (_guidList.Contains(guid))
                throw new InvalidOperationException($"GUID {guid} already exist in progress");

            _guidList.Add(guid);
        }

        public static ListProgressData Load(string key)
        {
            if (PlayerPrefs.HasKey(key) == false)
                return new ListProgressData();

            var json = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<ListProgressData>(json);
        }

        public void Save(string key)
        {
            var json = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(key, json);
        }
    }
}
