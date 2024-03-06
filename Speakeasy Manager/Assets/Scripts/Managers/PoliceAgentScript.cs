using UnityEngine;
using UnityEngine.AI;

public class PoliceAgentScript : MonoBehaviour
{
    public float idlePercentage;
    public LayerMask targetLayer;
    public float moveRadius;

    private PoliceManager policeManager;
    private NavMeshAgent agent;
    private GameObject[] targetObjects;

    void Start()
    {
        policeManager = PoliceManager.instance;
        agent = GetComponent<NavMeshAgent>();
        targetObjects = FindObjectsOfType<GameObject>();

        if (Random.value * 100 < idlePercentage)
        {
            MoveToRandomPosition();
        }
        else
        {
            MoveToTarget();
        }
    }

    void MoveToRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
        randomDirection += new Vector3(0,1,0);
        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, moveRadius, 1);
        Vector3 finalPosition = hit.position;
        agent.SetDestination(finalPosition);
    }

    void MoveToTarget()
    {
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject target in targetObjects)
        {
            if (((1 << target.layer) & targetLayer) != 0)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }
        }

        if (closestTarget != null)
        {
            agent.SetDestination(closestTarget.transform.position);
        }
        else
        {
            Debug.LogWarning("No target found on specified layer!");
        }
    }
}
