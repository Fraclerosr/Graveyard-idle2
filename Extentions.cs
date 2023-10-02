using System.Collections.Generic;
using UnityEngine;

public static class Extentions
{
    public static GameObject[] Clear(this StackPresenter stackPresenter, StackableType type)
    {
        var removedObjects = new List<GameObject>(stackPresenter.Count);
        while (stackPresenter.CalculateCount(type) != 0)
        {
            var stackable = stackPresenter.RemoveFromStack(type);
            removedObjects.Add(stackable.gameObject);
        }

        return removedObjects.ToArray();
    }

    public static void Clear(this StackPresenter stackPresenter)
    {
        foreach (Stackable stackable in stackPresenter.RemoveAll())
            Object.Destroy(stackable.gameObject);
    }
}
