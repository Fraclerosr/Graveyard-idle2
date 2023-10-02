using System.Collections.Generic;
using UnityEngine;

//todo split references for different characters
public class CharacterReferences : MonoBehaviour
{
    [SerializeField] private List<TimerStackableProducer> _producers;
    
    [field: SerializeField] public ShopQueues ShopQueues { get; private set; }
    [field: SerializeField] public CustomerQueue GraveyardQueue { get; private set; }
    [field: SerializeField] public CustomerQueue CrematoryQueue { get; private set; }
    [field: SerializeField] public CashDeskList CashDesks { get; private set; }
    [field: SerializeField] public ConveyorsContainer ConveyorsContainer { get; private set; }
    [field: SerializeField] public Transform ExitPoint { get; private set; }
    [field: SerializeField] public Conveyor CrematoryConveyor { get; private set; }
    [field: SerializeField] public Transform UrnStorage { get; private set; }
    [field: SerializeField] public StackPresenter UrnStack { get; private set; }
    [field: SerializeField] public GravesContainer GravesContainer { get; private set; }

    public IEnumerable<TimerStackableProducer> Producers => _producers;
}