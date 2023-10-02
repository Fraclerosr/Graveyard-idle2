using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
public class NavMeshObstacleSetup : MonoBehaviour
{
    private const float EnableDelay = 2f;

    private NavMeshObstacle _obstacle;

    private void Awake()
    {
        _obstacle = GetComponent<NavMeshObstacle>();
        _obstacle.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(Enable());
    }

    private IEnumerator Enable()
    {
        yield return new WaitForSeconds(EnableDelay);

        _obstacle.enabled = true;
    }
}
