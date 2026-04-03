using UnityEngine;
using UnityEngine.AI;

public class TowerMovemnet : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    public int TowerRange = 3;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 pos)
    {
        navMeshAgent.SetDestination(pos);
    }

    public bool HasReachedDestination()
    {
        return navMeshAgent.remainingDistance < GameManager.MIN_MOVE_DIST;
    }

}
