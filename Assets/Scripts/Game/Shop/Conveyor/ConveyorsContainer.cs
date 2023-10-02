using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ConveyorsContainer
{
    [SerializeField] private List<Conveyor> _conveyors;
    public IEnumerable<Conveyor> Conveyors => _conveyors;

    public void Add(Conveyor conveyor) =>
        _conveyors.Add(conveyor);

    public Conveyor GetByProducibleType(StackableType type) =>
        _conveyors?.FirstOrDefault(conveyor => conveyor.ProducibleType == type);
    
    public Conveyor GetByResourceType(StackableType type) =>
        _conveyors?.FirstOrDefault(conveyor => conveyor.ResourceType == type);

    public IEnumerable<StackableType> GetProducibleTypes() => 
        _conveyors.Select(conveyor => conveyor.ProducibleType);

    public IEnumerable<StackableType> GetResourceTypes() =>
        _conveyors.Select(conveyor => conveyor.ResourceType);
}