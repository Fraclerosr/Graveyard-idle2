using System;
using UnityEngine;

namespace BabyStack.Settings
{
    public abstract class Setting : ISetting
    {
        private readonly string SaveKey;

        public Setting(string saveKey)
        {
            SaveKey = saveKey;
        }

        public bool IsEnable => Convert.ToBoolean(PlayerPrefs.GetInt(SaveKey, 1));

        public void Enable()
        {
            SetActive(true);
        }

        public void Disable()
        {
            SetActive(false);
        }

        private void SetActive(bool value)
        {
            PlayerPrefs.SetInt(SaveKey, Convert.ToInt32(value));
        }
    }
}