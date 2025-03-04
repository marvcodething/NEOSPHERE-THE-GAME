using UnityEngine;
using UnityEngine.UI;

public class BlastAttack : MonoBehaviour
{
    public Transform player;
    public ParticleSystem particleSystem;

    private float timer = 0f; // Timer to track time since last F key press
    public float cooldownDuration = 10f; // Cooldown duration in seconds
    public float range = 5f; // Define the range for the attack

    public Image blastMeter;
    public Sprite[] blastMeterSprites;

    void Start()
    {
        UpdateBlastMeter(); // Initialize the blast meter on start
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);

        // Update the timer
        if (timer > 0)
        {
            timer -= Time.deltaTime; // Decrease timer by the time passed since last frame
            UpdateBlastMeter(); // Update the blast meter based on the timer
        }

        if (Input.GetKeyDown(KeyCode.F) && timer <= 0) // Check if the F key is pressed and timer is up
        {
            PlayParticleSystem(); // Call the method to play the particle system
            AttackEnemies(); // Call the method to attack enemies
            timer = cooldownDuration; // Reset the timer
        }
    }

    void PlayParticleSystem()
    {
        if (particleSystem != null) // Ensure the particle system is assigned
        {
            particleSystem.Play(); // Play the particle system
        }
    }

    void AttackEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy")) // Use "Enemy" as a string to compare the tag
            {
                EnemyDeath enemyDeath = hitCollider.GetComponent<EnemyDeath>(); // Get the EnemyDeath component
                if (enemyDeath != null) // Check if the component exists
                {
                    enemyDeath.enemyHealth = 0; // Decrease enemy health
                    enemyDeath.updateHealthBar(); // Update the health bar
                    enemyDeath.PlayDeathParticles(); // Play death particles if health is zero
                }
            }
        }
    }

    void UpdateBlastMeter()
    {
        // Calculate the blast meter index based on the remaining timer
        int spriteIndex = Mathf.Clamp((int)((1 - (timer / cooldownDuration)) * (blastMeterSprites.Length - 1)), 0, blastMeterSprites.Length - 1);
        blastMeter.sprite = blastMeterSprites[spriteIndex]; // Update the blast meter image
    }
}
