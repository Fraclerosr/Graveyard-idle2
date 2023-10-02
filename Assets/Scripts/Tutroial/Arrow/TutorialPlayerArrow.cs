using UnityEngine;

public class TutorialPlayerArrow : MonoBehaviour, ITutorialArrow
{
    [SerializeField] private GameObject _arrow;

    private Transform _target;

    private void Awake()
    {
        _arrow.SetActive(false);
    }

    private void Update()
    {
        if (_arrow.activeSelf == false)
            return;

        _arrow.transform.LookAt(_target);
        _arrow.transform.rotation = Quaternion.Euler(0, _arrow.transform.eulerAngles.y, 0);
    }

    public void SetTarget(Transform target)
    {
        if (target == null)
            _arrow.SetActive(false);
        else
            _arrow.SetActive(true);

        _target = target;
    }
}
