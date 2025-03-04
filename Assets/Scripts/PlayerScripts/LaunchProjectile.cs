using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    // Public variables
    public GameObject projectile;
    public float launchForce = 700f;
    public float lifespan = 1f; // Time before the projectile disappears
    public Transform projectilesParent; // Assign the Projectiles empty GameObject in the Inspector
    public ParticleSystem launchFlash;

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code here
    }

    // Update is called once per frame
    void Update()
    {
        // Handle input and launch projectile
        if (Input.GetButtonDown("Fire1"))
        {
            Launch();
        }
        // Launch projectile using space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
    }

    void Launch()
    {
        // Instantiate the projectile and apply force
        GameObject ball = Instantiate(projectile, transform.position, transform.rotation);
        ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, launchForce, 0));

        // Ignore collisions with the player and its children
        Collider playerCollider = GetComponent<Collider>();
        if (playerCollider != null)
        {
            Physics.IgnoreCollision(ball.GetComponent<Collider>(), playerCollider);
            foreach (Collider childCollider in GetComponentsInChildren<Collider>())
            {
                Physics.IgnoreCollision(ball.GetComponent<Collider>(), childCollider);
            }
        }
        // Activate muzzle flash
        launchFlash.Play();

        // Organize the projectile in the hierarchy
        ball.name = "LaunchedBall"; // Set a name for better organization
        ball.transform.parent = projectilesParent; // Set the parent to the Projectiles GameObject
        Destroy(ball, lifespan); // Destroy the projectile after a specified lifespan
    }
}
