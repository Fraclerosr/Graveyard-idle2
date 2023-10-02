using UnityEngine;

public class UrnContainer : MonoBehaviour
{
    [SerializeField] private StackPresenter _stackPresenter;
    [SerializeField] private IntProgress _urnProgress;
    [SerializeField] private Urn _urnPrefab;

    private void OnEnable()
    {
        if(_stackPresenter.Count == 0)
            CreateSavedUrns();
        
        _stackPresenter.Added += OnAdded;
    }

    private void OnDisable() => 
        _stackPresenter.Added -= OnAdded;

    private void CreateSavedUrns()
    {
        for (int i = 0; i < _urnProgress.CurrentProgress; i++)
        {
            Urn urn = Instantiate(_urnPrefab, transform);
            _stackPresenter.AddToStack(urn);
        }
    }

    private void OnAdded(Stackable stackable)
    {
        if (stackable.Type == StackableType.Urn && _stackPresenter.Count > _urnProgress.CurrentProgress)
        {
            _urnProgress.Add();
            _urnProgress.Save();
        }
    }
}