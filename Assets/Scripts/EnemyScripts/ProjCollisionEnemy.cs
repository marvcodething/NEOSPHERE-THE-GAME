using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int healthAmount; // Damage dealt to the player

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            // Handle collision with the player
            Debug.Log("Hit the player!");
            // Add additional logic for when the projectile hits the player
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.currHealth -= healthAmount; // Decrease player's health by healthAmount
                playerController.UpdateHealthText(); // Update the health text display
            } else{
                Debug.Log("Player Controller Returns Null");
            }
            Destroy(gameObject); // Destroy the projectile upon collision
        }
        else
        {
            // Destroy the projectile if it hits anything else
            Destroy(gameObject);
        }
    }
}