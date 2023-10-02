using BabyStack.Model;
using System.Collections.Generic;
using UnityEngine;

public class AssistantStackCapacityModificationPresenter : ModificationPresenter<AssistantStackCapacityModification, int>
{
    [SerializeField] private List<AssistantsContainer> _containers;

    protected override void Enabled()
    {
        foreach (var container in _containers)
            container.Spawned += OnDoctorSpawned;
    }

    protected override void Disabled()
    {
        foreach (var container in _containers)
            container.Spawned -= OnDoctorSpawned;
    }

    private void OnDoctorSpawned(Assistant assistant)
    {
        var stack = assistant.GetComponent<StackPresenter>();
        AddListener(stack);
    }
}
