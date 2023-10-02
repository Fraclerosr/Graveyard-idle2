using System.Collections.Generic;
using UnityEngine;

public class PurchaseListView : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private PurchaseView _purchaseViewTemplate;

    private List<PurchaseView> _purchaseViews = new List<PurchaseView>();
    private PurchaseList _purchaseList;

    public void Init(PurchaseList purchaseList)
    {
        _purchaseList = purchaseList;

        UpdateView();
    }
    
    public void UpdateView()
    {
        if (_purchaseViews != null)
        {
            foreach (PurchaseView purchaseView in _purchaseViews)
                Destroy(purchaseView.gameObject);

            _purchaseViews.Clear();
        }

        _purchaseViews = new List<PurchaseView>();
        
        foreach (KeyValuePair<StackableType, int> item in _purchaseList.Items)
        {
            PurchaseView view = Instantiate(_purchaseViewTemplate, _container);
            _purchaseViews.Add(view);
            view.Display(item.Key, item.Value);
        }
    }
}