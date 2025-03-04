using UnityEngine;

public class HealthRotation : MonoBehaviour
{
    private Quaternion initialRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Store the initial rotation of the health bar
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Lock the health bar rotation to its initial rotation
        transform.rotation = initialRotation;
    }
}
