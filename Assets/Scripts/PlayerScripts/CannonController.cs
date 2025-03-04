using UnityEngine;

public class CannonController : MonoBehaviour
{
    // Speed of rotation
    public float rotationSpeed = 300f;

    // Reference to the player transform
    public Transform playerTransform; // Make this public to allow dragging in the inspector

    // Offset from the player's position
    public Vector3 positionOffset; // Allow setting an offset in the inspector

    // Update is called once per frame
    void Update()
    {
        // Check for input to spin the cannon disk clockwise or counterclockwise around its own y-axis
        if (Input.GetKey(KeyCode.J))
        {
            // Rotate counterclockwise
            transform.GetChild(0).Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.K))
        {
            // Rotate clockwise
            transform.GetChild(0).Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        // Update the cannon's position to follow the player with an offset
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + positionOffset;
        }
    }
}
