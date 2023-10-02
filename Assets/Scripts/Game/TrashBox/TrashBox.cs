using UnityEngine;
using DG.Tweening;

public class TrashBox : MonoBehaviour
{
    [SerializeField] private Transform _trashEndPoint;
    [SerializeField] private StackableTypes _trashTypes;
    [SerializeField] private float _moveTrashDuration;

    public StackableTypes TrashTypes => _trashTypes;

    public void Add(Stackable stackable)
    {
        if (_trashTypes.Contains(stackable.Type) == false)
            throw new System.InvalidOperationException("Can't add");

        stackable.transform.DOComplete(true);
        stackable.transform.parent = null;

        stackable.transform.DOMove(_trashEndPoint.position, _moveTrashDuration).OnComplete(() => Destroy(stackable.gameObject));
    }
}
