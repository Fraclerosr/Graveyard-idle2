using BabyStack.Model;
using UnityEngine;

public class PlayerSpeedModificationPresenter : ModificationPresenter<PlayerSpeedRateModification, float>
{
    [SerializeField] private PlayerMovement _movement;

    protected override void Enabled()
    {
        AddListener(_movement);
    }
}