using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MoneyMagnit : MonoBehaviour
{
    [SerializeField] private float _attractDuration = 1f;
    [SerializeField] private float _followOffsetDistance = 5f;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _scaleReduceDuration = 0.5f;
    [SerializeField] private float _scaleReduceMoveSpeed = 5;
    [SerializeField] private float _takeShakeDuration = 0.2f;

    private float _followRange => _followOffsetDistance * _followOffsetDistance;

    public event Action<int> Attracted;

    public void AttractLinear(Dollar dollar)
    {
        dollar.transform.DOComplete(true);

        dollar.transform.DOLocalRotate(Vector3.zero, 0.05f);
        dollar.transform.DOLocalMove(transform.position, 0.05f).OnComplete(() =>
        {
            Attracted?.Invoke(dollar.Value);
            Destroy(dollar.gameObject);
        });
    }

    public void Attract(Dollar dollar)
    {
        dollar.DisableCollision();
        StartCoroutine(Animate(dollar));
    }

    private IEnumerator Animate(Dollar dollar)
    {
        dollar.transform.DOComplete(true);
        dollar.transform.DOShakeScale(_takeShakeDuration, 4f);

        yield return new WaitForSeconds(_takeShakeDuration);

        dollar.transform.DOLocalRotate(Vector3.zero, _attractDuration);

        while (Vector3.SqrMagnitude(transform.position - dollar.transform.position) > _followRange)
        {
            float clampedSpeed = Mathf.Clamp(_speed * Time.deltaTime, 0, 1);
            dollar.transform.position = Vector3.Lerp(dollar.transform.position, transform.position, clampedSpeed);

            yield return null;
        }

        dollar.transform.DOScale(0, _scaleReduceDuration).OnComplete(() =>
        {
            Attracted?.Invoke(dollar.Value);
            dollar.transform.DOComplete(true);
            Destroy(dollar.gameObject);
        });

        while (dollar)
        {
            dollar.transform.position = Vector3.MoveTowards(dollar.transform.position, transform.position,
                _scaleReduceMoveSpeed * Time.deltaTime);

            yield return null;
        }
    }
}