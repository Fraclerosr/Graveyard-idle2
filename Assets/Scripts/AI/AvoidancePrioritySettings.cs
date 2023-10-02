using UnityEngine.AI;

public class AvoidancePrioritySettings : Singleton<AvoidancePrioritySettings>
{
    private int _priority = 0;

    public void SetPriority(NavMeshAgent agent)
    {
        agent.avoidancePriority = _priority;
        _priority++;

        if (_priority > 99)
            _priority = 0;
    }
}
