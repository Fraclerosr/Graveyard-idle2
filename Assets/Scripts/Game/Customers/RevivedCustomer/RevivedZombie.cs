using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RevivedZombie : RevivedCustomer
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    
    protected override IEnumerator DigCoffin()
    {
        _navMeshAgent.enabled = false;
        Model.gameObject.SetActive(true);
        yield return null;
    }

    protected override IEnumerator OnRun()
    {
        _navMeshAgent.enabled = true;
        Run();
        yield return new WaitForSeconds(1f);
        GraveEarth.DigUp(3f);
        yield return new WaitForSeconds(1f);
    }
}