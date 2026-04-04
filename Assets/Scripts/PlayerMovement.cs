using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    public int MoveRange = 5;

    public int AttackRange = 1;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 pos)
    {
        navMeshAgent.SetDestination(pos);
    }

    internal bool HasReachedDestination()
    {
        return navMeshAgent.remainingDistance < GameManager.MIN_MOVE_DIST;
    }
}
