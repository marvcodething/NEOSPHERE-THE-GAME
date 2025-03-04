using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;
    public float activationRange = 10f; // Distance within which the enemy activates

    public bool isActive = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = false; // Disable the NavMeshAgent initially
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= activationRange && !isActive) // Check if within range and not active
            {
                navMeshAgent.enabled = true; // Enable the NavMeshAgent when the player is close
                isActive = true; // Set isActive to true
            }
            if (isActive) {
                navMeshAgent.SetDestination(player.position); // Continue following the player
            }
        }
    }
}
