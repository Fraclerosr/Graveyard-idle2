using UnityEngine;

public class DuckyID
{
    private const string DuckyIDKey = "DuckyID";

    public string Value()
    {
        var value = PlayerPrefs.GetString(DuckyIDKey, string.Empty);

        if (string.IsNullOrEmpty(value) == false)
            return value;

        value = "Ducky_" + SystemInfo.deviceUniqueIdentifier;
        PlayerPrefs.SetString(DuckyIDKey, value);

        return value;
    }
}
