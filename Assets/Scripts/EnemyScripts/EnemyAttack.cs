using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject projectile; // Assign projectile (ideas other than spheres ! ! ! )
    public GameObject target; // Should always be player object
    public float speed = 300f;
    public float lifespan = 1f; // lifespan value
    public Transform projParent; // <- for navigator organization
    public float attackRange = 100f; // Range within which the enemy can attack
    private bool inRange = false;
    public float attackDelay = 0.5f; // Delay between attacks
    private float lastAttackTime = 0f; // Time of the last attack

    private EnemyMovement enemyMovement; // Reference to the EnemyMovement script

    void Start()
    {
        enemyMovement = GetComponentInParent<EnemyMovement>(); // Get the EnemyMovement component from the parent
    }

    void Update()
    {
        // Only proceed if the enemy is active
        if (enemyMovement != null && enemyMovement.isActive)
        {
            // Check if the target is within range
            if (target && Vector3.Distance(transform.position, target.transform.position) <= attackRange)
            {
                inRange = true;
                if (target != null)
                {
                    // Assuming the target has a Canvas component as a child
                    Canvas canvas = target.GetComponentInChildren<Canvas>();
                    if (canvas != null)
                    {
                        canvas.gameObject.SetActive(true); // Activate the canvas child object
                    }
                    else
                    {
                        Debug.LogWarning("No Canvas component found in the target's children.");
                    }
                }
            }
            else
            {
                inRange = false;
            }

            // Fire projectiles if in range and the attack delay has passed
            if (inRange && Time.time >= lastAttackTime + attackDelay)
            {
                TargetAndFire(target.transform.position);
                lastAttackTime = Time.time; // Update the last attack time
            }
        }
    }

    void TargetAndFire(Vector3 targetPosition)
    {
        // Calculate the direction to the target
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;

        // Fire projectiles in the direction of the target and at 45-degree angles
        FireProjectile(directionToTarget);
        FireProjectile(Quaternion.Euler(0, 45, 0) * directionToTarget);
        FireProjectile(Quaternion.Euler(0, -45, 0) * directionToTarget);
    }

    void FireProjectile(Vector3 direction)
    {
        // Instantiate the projectile slightly offset to prevent immediate collisions
        GameObject launchedProjectile = Instantiate(projectile, transform.position + direction * 0.5f, Quaternion.identity, projParent);

        // Add force to the projectile in the specified direction
        Rigidbody projectileRigidbody = launchedProjectile.GetComponent<Rigidbody>();
        if (projectileRigidbody != null)
        {
            projectileRigidbody.AddForce(direction * speed);
        }

        // Destroy the projectile after its lifespan expires
        Destroy(launchedProjectile, lifespan);

        // Ignore collisions between the projectile and the enemy that fired it
        Collider enemyCollider = GetComponent<Collider>();
        Collider projectileCollider = launchedProjectile.GetComponent<Collider>();
        if (enemyCollider != null && projectileCollider != null)
        {
            Physics.IgnoreCollision(enemyCollider, projectileCollider);
        }
    }
}