using UnityEngine;
using TMPro; // Only if using TextMeshPro

public class TitleController : MonoBehaviour
{
    public float minVisibleTime = 0.5f; // Minimum time the text is visible
    public float maxVisibleTime = 1.5f; // Maximum time the text is visible
    public float flashDuration = 0.1f; // Duration for which the text will flash
    private float timer;
    private float visibleTime; // Randomized visible time
    private TextMeshProUGUI textMesh; // Use TextMeshPro
    // private Renderer textRenderer; // Use this instead if using a 3D TextMesh

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>(); // Get the text component
        // textRenderer = GetComponent<Renderer>(); // If using a 3D text object
        SetRandomVisibleTime(); // Set initial random visible time
        textMesh.enabled = true; // Ensure text is visible at start
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > visibleTime)
        {
            textMesh.enabled = false; // Corrected from 'disabled' to 'enabled = false'
            if (timer >= flashDuration) // Check if the flash duration has passed
            {
                textMesh.enabled = !textMesh.enabled; // Toggle visibility
                timer = 0f;
                SetRandomVisibleTime(); // Set a new random visible time
            }
        }
    }

    private void SetRandomVisibleTime()
    {
        visibleTime = Random.Range(minVisibleTime, maxVisibleTime); // Randomize visible time
    }
}
