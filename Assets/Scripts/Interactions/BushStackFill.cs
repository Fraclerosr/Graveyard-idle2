using System.Collections;
using UnityEngine;

public class BushStackFill : MonoBehaviour
{
    [SerializeField] private StackPresenter _stackPresenter;
    [SerializeField] private SingleTypeRandomStackableProvider _provider;
    [SerializeField] private TransformListStackHolder _stackHolder;
    [SerializeField] private float _growDuration = 3.0f;

    private void Start()
    {
        StartCoroutine(Add());
    }

    private IEnumerator Add()
    {
        while (true)
        {
            yield return new WaitUntil(() => _stackPresenter.IsFull == false);
            yield return new WaitForSeconds(_growDuration);

            Transform freePlace = _stackHolder.GetNextFreePlace();

            if(freePlace == null)
                continue;

            Stackable stackable = _provider.InstantiateStackable();
            stackable.transform.position = freePlace.position;
            _stackPresenter.AddToStack(stackable);
        }
    }
}