using System;
using UnityEngine;

[Serializable]
public class IntentionTypeView
{
    [field: SerializeField] public IntentionType IntentionType { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}