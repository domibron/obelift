using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : EnemyBase
{
    private NavMeshAgent navMeshAgent;

    private Transform tower;
    private Transform player;
    private GridSystem gridSystem;

    private Transform target;

    private int AttackRange = 3;
    private int MoveRange = 3;

    Coroutine attackCo;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    public override void StartMove()
    {
        if (attackCo != null)
        {

            Debug.Log("You are calling enemy attack start more than once!");
            return;
        }

        if (gridSystem.GetDistanceOnGrid(transform.position, tower.position) < gridSystem.GetDistanceOnGrid(transform.position, player.position))
        {
            target = tower;
        }
        else
        {
            target = player;
        }

        attackCo = StartCoroutine(MoveAndAttack());
    }

    IEnumerator MoveAndAttack()
    {
        int distToTarget = gridSystem.GetDistanceOnGrid(transform.position, target.position);

        if (distToTarget <= AttackRange)
        {
            // attack.
            print("BOOM");
            Attack();
        }
        else
        {
            Vector2Int targetGridPos = gridSystem.GetGridPosRound(target.position);
            Vector2Int currentGridPos = gridSystem.GetGridPosRound(transform.position);
            Vector2Int dir = targetGridPos - currentGridPos;
            dir = new Vector2Int((dir.x != 0 ? (dir.x > 0 ? 1 : -1) : 0), (dir.y != 0 ? (dir.y > 0 ? 1 : -1) : 0));
            int x = (targetGridPos.x - dir.x) - currentGridPos.x;
            int y = (targetGridPos.y - dir.y) - currentGridPos.y;

            if (x > MoveRange) x = MoveRange;
            else if (x < -MoveRange) x = -MoveRange;

            if (y > MoveRange) y = MoveRange;
            else if (y < -MoveRange) y = -MoveRange;

            Vector2Int targetSquare = currentGridPos + new Vector2Int(x, y);

            Debug.DrawLine(gridSystem.GetGridPosAsWrold(targetSquare), gridSystem.GetGridPosAsWrold(targetSquare) + Vector3.up, Color.cyan, 10f);

            if (gridSystem.CheckGridIsEmpty(targetSquare, Constants.GRID_BLOCKER_LAYER))
                navMeshAgent.SetDestination(gridSystem.GetGridPosAsWrold(targetSquare));
        }

        yield return new WaitForSeconds(1);
        AttackEnd();
    }

    void Attack()
    {
        Collider[] cols = gridSystem.GetCollidersInRange(transform.position, AttackRange, Constants.GRID_BLOCKER_LAYER);

        if (cols.Length <= 0)
        {
            print("No enemies");
            return;
        }

        foreach (Collider col in cols)
        {
            if (col.gameObject.CompareTag(Constants.PLAYER_TAG) || col.gameObject.CompareTag(Constants.TOWER_TAG))
            {
                col.transform.GetComponent<Health>()?.TakeFromHealth(10f);
                print("Get fucked");
                print(col.transform.name);
            }
        }
    }

    public override void SetUpEnemy(GameManager gManager)
    {
        base.SetUpEnemy(gManager);

        tower = gManager.TowerMovemnet.transform;
        player = gManager.PlayerMovement.transform;
        gridSystem = gManager.GridSystem;
    }

    protected override void AttackEnd()
    {
        base.AttackEnd();

        attackCo = null;
    }



}
