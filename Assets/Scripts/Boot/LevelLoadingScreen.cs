using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoadingScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _loadingText;
    [SerializeField] private Slider _processSlider;
    [SerializeField] private Image _loadingImage;

    private Coroutine _loadOperation;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName, string loadingText, Action onLoaded = null)
    {
        if (_loadOperation != null)
            throw new InvalidOperationException("Another scene is already loading");

        _loadingText.text = loadingText;
        _loadOperation = StartCoroutine(LoadSceneOperation(sceneName, onLoaded));
    }

    private IEnumerator LoadSceneOperation(string sceneName, Action onLoaded)
    {
        _loadingImage.transform.DORotate(Vector3.forward * 360, 1f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        var operation = SceneManager.LoadSceneAsync(sceneName);
        
        while (operation.isDone == false)
        {
            yield return null;
            _processSlider.value = operation.progress;
        }

        _loadingImage.transform.DOKill(true);

        onLoaded?.Invoke();
    }
}
