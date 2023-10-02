using System.Linq;
using UnityEngine;

public class CashDeskList : ReferenceObjectList<CashDesk>
{
    [SerializeField] private ListProgress _cashDeskProgress;

    protected override void AfterUnlocked(CashDesk reference, bool onLoad, string guid)
    {
        if (_cashDeskProgress.Contains(guid))
            return;
        
        _cashDeskProgress.Add(guid);
        _cashDeskProgress.Save();
    }

    public CashDesk GetFreeDesk() => 
        Data.FirstOrDefault(desk => desk.Free);
}