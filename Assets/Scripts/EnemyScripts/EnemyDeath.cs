using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // Added to include the Image class

public class EnemyDeath : MonoBehaviour
{
    public int enemyHealth;
    
    public ParticleSystem deathParticleSystem; // Reference to the particle system

    public Sprite[] healthBarSprites; // Array to hold health bar sprites
    public Image healthBarImage; // Reference to the health bar
    private int maxHealth; // Moved declaration outside of Start

    void Start()
    {
        maxHealth = enemyHealth; // Initialize maxHealth
        updateHealthBar(); // Initialize health bar on start
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player Proj")) {
            enemyHealth--; // Decrease enemy health when hit by a player projectile
            Debug.Log("Enemy hit by player projectile. Current health: " + enemyHealth);
            updateHealthBar(); // Update health bar when health changes
            if (enemyHealth <= 0) {
                PlayDeathParticles(); // Play particle system on death
                Invoke("DestroyEnemy", 0.5f); // Wait 0.5 seconds before destroying the enemy
            }
        }
    }

    void Update()
    {
        if (enemyHealth <= 0) {
            Invoke("DestroyEnemy", 0.2f); // Wait 0.2 seconds before destroying the enemy
        }
    }
    
    public void updateHealthBar()
    {
        // Calculate the adjusted health for the health bar based on the number of sprites
        int spriteIndex = Mathf.Clamp((enemyHealth * (healthBarSprites.Length - 1)) / maxHealth, 0, healthBarSprites.Length - 1);

        // Update the HealthBar image based on the calculated sprite index
        healthBarImage.sprite = healthBarSprites[spriteIndex];
    }

    public void PlayDeathParticles() {
        if (deathParticleSystem != null) {
            deathParticleSystem.Play(); // Play the particle system
        } else {
            Debug.Log("No Particle System");
        }
    }

    void DestroyEnemy() {
        Destroy(gameObject); // Destroy the enemy
    }
}
