using System;
using UnityEngine;

public class Assistant : AICharacter
{
    public Transform[] WaitPoints { get; private set; }

    public void Init(Transform[] waitPoints)
    {
        WaitPoints = waitPoints;
    }
}