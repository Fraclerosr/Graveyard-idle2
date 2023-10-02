using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CreatureRevive : MonoBehaviour
{
    [SerializeField] private Animator _coffinAnimator;
    [SerializeField] private GraveEarth _graveEarth;
    [SerializeField] private NavMeshObstacle _obstacle;

    private CharacterReferences _references;
    private RevivedCustomer _customerTemplate;

    public void Init(CharacterReferences characterReferences, RevivedCustomer template)
    {
        _references = characterReferences;
        _customerTemplate = template;
    }

    public IEnumerator Revive()
    {
        yield return StartCoroutine(Activate());
    }
    
    private IEnumerator Activate()
    {
        _obstacle.enabled = false;
        RevivedCustomer revivedCustomer = Instantiate(_customerTemplate, transform);
        revivedCustomer.Init(_graveEarth, _coffinAnimator, _references);
        yield return revivedCustomer.StartRevive();
        _obstacle.enabled = true;

        yield return null;
    }
}