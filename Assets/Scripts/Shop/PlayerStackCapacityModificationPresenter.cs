using UnityEngine;
using BabyStack.Model;

public class PlayerStackCapacityModificationPresenter : ModificationPresenter<PlayerStackCapacityModification, int>
{
    [SerializeField] private StackPresenter _playerStack;

    protected override void BeforeStart()
    {
        AddListener(_playerStack);
    }
}
