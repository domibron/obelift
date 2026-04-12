using UnityEngine;
using UnityEngine.AI;

public class TowerMovemnet : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    public int TowerRange = 3;

    [SerializeField] Transform goal;
    [SerializeField] Transform compass;

    private bool isCompassVisible = false;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        compass.gameObject.SetActive(isCompassVisible);

        if (isCompassVisible)
        {
            Vector3 targetDir = GetGoalPos() - transform.position;
            targetDir.y = 0;

            float angle = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;

            compass.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    public void SetCompasVisible(bool b)
    {
        isCompassVisible = b;
    }

    public void MoveTo(Vector3 pos)
    {
        navMeshAgent.SetDestination(pos);
    }

    public bool HasReachedDestination()
    {
        return navMeshAgent.remainingDistance < GameManager.MIN_MOVE_DIST;
    }

    public Vector3 GetGoalPos()
    {
        return goal.position;
    }
}
