using System.Linq;
using UnityEngine;

public class CoffinBouquetMerger : StackItemsMerger
{
    protected override void OnMerged(Stackable firstItem, Stackable secondItem, Stackable newItem)
    {
        if (!(newItem is Coffin coffin)) 
            return;
        
        TryInitCoffin(coffin, firstItem); 
        TryInitCoffin(coffin, secondItem);
    }

    private bool TryInitCoffin(Coffin coffin, Stackable firstItem)
    {
        if (!(firstItem is Coffin deadBody))
            return false;
        
        coffin.Init(deadBody.UrnTemplate);
        
        return true;
    }
}