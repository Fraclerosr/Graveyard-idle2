using UnityEngine;

public class TutorialObjectArrow : MonoBehaviour, ITutorialArrow
{
    [SerializeField] private GameObject _template;

    private GameObject _spawnedArrow;

    private void Awake()
    {
        _spawnedArrow = Instantiate(_template);
        _spawnedArrow.SetActive(false);
    }

    public void SetTarget(Transform target)
    {
        if (target == null)
            _spawnedArrow.SetActive(false);
        else
            _spawnedArrow.SetActive(true);
        
        _spawnedArrow.transform.SetParent(target);
        _spawnedArrow.transform.localPosition = Vector3.zero;
    }
}
