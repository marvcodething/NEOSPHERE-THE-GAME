using UnityEngine;
using UnityEngine.AI; // Added to include NavMeshAgent

public class CollectibleController : MonoBehaviour // Changed class name to follow C# naming conventions
{
    private Transform player; // Declare player variable
    private NavMeshAgent myNMagent; // Declare NavMeshAgent variable
    private float nextTurnTime; // Declare nextTurnTime variable
    public float multiplyBy = 10f; // Added multiplyBy variable with a default value
    public float detectionRadius = 15f; // Added detection radius for player proximity
    private float runInterval = 2f; // Added runInterval variable to control how often to run away

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Use null-conditional operator to prevent crashes
        myNMagent = this.GetComponent<NavMeshAgent>();

        if (player != null) // Check if player is found before running
        {
            RunFrom();
        }
        else
        {
            Debug.LogWarning("Player not found! Make sure the player has the 'Player' tag.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check distance to player and run if within detection radius
        if (player != null && Vector3.Distance(transform.position, player.position) < detectionRadius)
        {
            if (Time.time > nextTurnTime) // Check if it's time to run again
            {
                RunFrom();
                nextTurnTime = Time.time + runInterval; // Set next turn time based on runInterval
            }
        }
    }

    public void RunFrom()
    {
        // temporarily point the object to look away from the player
        transform.rotation = Quaternion.LookRotation(transform.position - player.position);

        // Then we'll get the position on that rotation that's multiplyBy down the path
        Vector3 runTo = transform.position + transform.forward * multiplyBy;

        // So now we've got a Vector3 to run to and we can transfer that to a location on the NavMesh with samplePosition.
        NavMeshHit hit; // stores the output in a variable called hit

        // 5 is the distance to check, assumes you use default for the NavMesh Layer name
        if (NavMesh.SamplePosition(runTo, out hit, 5, NavMesh.AllAreas))
        {
            // And get it to head towards the found NavMesh position
            myNMagent.SetDestination(hit.position);
        }
        else
        {
            // Decrease multiplyBy if no valid NavMesh position is found
            multiplyBy = Mathf.Max(multiplyBy - 2f, 1f); // Ensure multiplyBy doesn't go below 1
            Debug.LogWarning("No valid NavMesh position found for running away. Decreasing multiplyBy.");
        }

        // just used for testing - safe to ignore
        nextTurnTime = Time.time + 5;
    }
}
