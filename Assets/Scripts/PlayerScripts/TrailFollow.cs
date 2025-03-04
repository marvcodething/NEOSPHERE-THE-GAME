using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    public GameObject trailSegmentPrefab; // Assign the prefab in the Inspector
    public float pointSpacing = 0.5f; // Distance between trail segments
    public LayerMask groundLayer; // Assign the ground layer in the Inspector
    public float segmentSize = 0.1f; // Size of the trail segments
    public Transform trailParent; // Assign the empty trail object in the Inspector

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        // Add the player's current position to the trail if they've moved far enough
        if (Vector3.Distance(transform.position, lastPosition) > pointSpacing)
        {
            AddTrailSegment(transform.position);
            lastPosition = transform.position;
        }
    }

    void AddTrailSegment(Vector3 newPosition)
    {
        // Move the raycast origin slightly above the player to avoid hitting the player's collider
        Vector3 raycastOrigin = newPosition + Vector3.up * 0.2f; // Slightly higher to avoid glitches

        // Raycast downward to find the ground
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            // Use the hit point as the ground position
            Vector3 groundPosition = hit.point + Vector3.up * 0.01f; // Slightly above the ground

            // Instantiate a new trail segment at the ground position
            GameObject newSegment = Instantiate(trailSegmentPrefab, groundPosition, Quaternion.identity);

            // Set the segment's rotation to be flat to the ground
            newSegment.transform.rotation = Quaternion.Euler(90, 0, 0); // Pointing vertically

            // Scale the segment to make it a square
            newSegment.transform.localScale = new Vector3(segmentSize, segmentSize, segmentSize); // Making it a square

            // Organize the trail segment under the empty trail object in the hierarchy
            newSegment.transform.parent = trailParent; // Set the empty trail object as the parent of the trail segment

            // Debug: Draw a line to visualize the raycast
            Debug.DrawLine(raycastOrigin, hit.point, Color.green, 2f);
        }
        else
        {
            Debug.LogWarning("No ground detected below the player!");
            // Debug: Draw a line to visualize the raycast (in red if no hit)
            Debug.DrawLine(raycastOrigin, raycastOrigin + Vector3.down * 10f, Color.red, 2f);
        }
    }
}