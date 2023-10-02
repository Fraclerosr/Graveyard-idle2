using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AssistantsContainer : MonoBehaviour
{
    [SerializeField] private AssistantShopData _assistantsData;
    [SerializeField] private Transform _spawnContainer;
    [SerializeField] private float _spawnDelay = 0.5f;
    [SerializeField] private UnlockRule _refreshRule;
    [Space(15)]
    [SerializeField] private Transform[] _waitPoints;
    [SerializeField] private CharacterReferences _references;

    private List<string> _spawnedGuid;
    private AssistantInventory _inventory;

    public event UnityAction<Assistant> Spawned;

    private void OnEnable()
    {
        AssistantInventory.Added += OnAdded;

        if (_refreshRule.CanUnlock)
            Refresh();
        else
            _refreshRule.AddUpdateListener(OnUnlockRuleRefresh);
    }

    private void OnDisable()
    {
        AssistantInventory.Added -= OnAdded;

        if (_refreshRule.CanUnlock == false)
            _refreshRule.RemoveUpdateListener(OnUnlockRuleRefresh);
    }

    private void Awake()
    {
        _spawnedGuid = new List<string>();

        _inventory = new AssistantInventory(_assistantsData);
        _inventory.Load();

        StartCoroutine(SpawnWithDelay(_spawnDelay));
    }

    public Assistant Spawn(Assistant template)
    {
        var doctor = Instantiate(template, _spawnContainer);
        doctor.Init(_references);
        doctor.Init(_waitPoints);
        doctor.Run();

        return doctor;
    }

    public Assistant Spawn(Assistant template, Vector3 position)
    {
        var doctor = Spawn(template);
        doctor.Warp(position);

        return doctor;
    }

    private IEnumerator SpawnWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (var item in _inventory.Data)
            Spawn(item);
    }

    private void Spawn(AssistantData data)
    {
        var doctor = Spawn(data.Template);

        _spawnedGuid.Add(data.GUID);
        Spawned?.Invoke(doctor);
    }

    private void OnAdded(AssistantInventory otherInventory)
    {
        if (_inventory.Equals(otherInventory) == false)
            return;

        _inventory = otherInventory;

        foreach (var item in _inventory.Data)
        {
            if (_spawnedGuid.Contains(item.GUID))
                continue;

            Spawn(item);
        }
    }

    private void OnUnlockRuleRefresh()
    {
        if (_refreshRule.CanUnlock)
        {
            Refresh();
            _refreshRule.RemoveUpdateListener(OnUnlockRuleRefresh);
        }
    }

    private void Refresh()
    {
        foreach (var data in _assistantsData.Data)
        {
            if (data.AlwaysUnlocked && _inventory.Contains(data) == false)
            {
                _inventory.Add(data);
                _inventory.Save();
            }
        }
    }
}
