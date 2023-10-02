using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private Button _addButton;
    [SerializeField] private MoneyHolder _moneyHolder;
    [SerializeField] private ListProgress _motherBed;
    [SerializeField] private ListProgress _playpen;
    [SerializeField] private IntProgress _babyCount;
    [SerializeField] private IntProgress _milkCount;
    [SerializeField] private IntProgress _toyCount;
    [SerializeField] private IntProgress _cashCount;

    private float[] _timeScaleValues = { 0.1f, 1f, 2f, 4f};
    private int _currentTimeScale = -1;

    private void OnEnable()
    {
        _addButton.onClick.AddListener(OnAddButtonClicked);
    }

    private void OnDisable()
    {
        _addButton.onClick.RemoveListener(OnAddButtonClicked);
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _currentTimeScale = (_currentTimeScale + 1) % _timeScaleValues.Length;
            Time.timeScale = _timeScaleValues[_currentTimeScale];
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowProgress();
        }
    }
#endif
    public void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }

    private void OnAddButtonClicked()
    {
        _moneyHolder.AddMoney(200);
    }

    private void ShowProgress()
    {
        Debug.Log(new string('-', 30));
        Debug.Log("MotherBedProgress: " + _motherBed.CurrentProgress);
        Debug.Log("PlaypenProgress: " + _playpen.CurrentProgress);
        Debug.Log("BabyBornProgress: " + _babyCount.CurrentProgress);
        Debug.Log("TotalMoneyProgress: " + _cashCount.CurrentProgress);
        Debug.Log("TotalMangerItemProgress: Ball:" + _toyCount.CurrentProgress + "\tMilk:" + _milkCount.CurrentProgress);
        Debug.Log(new string('-', 30));
    }

    [ContextMenu("Delete Player Prefs")]
    public void DeletePrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
