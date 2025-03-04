using UnityEngine;

public class TrailDecay : MonoBehaviour
{
    public float poisonTime =  1f;
    private float timeExisted = 0f; // Variable to keep track of the time the object has existed

    public Material poisonMaterial; // Material to change to when poisoned
    private bool isPoison = false;

    public int healthAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    // Update is called once per frame

    void Update()
    {
        timeExisted += Time.deltaTime; // Increment the time existed by the time passed since the last frame
        if (timeExisted > poisonTime){
            isPoison = true ; 
        }
        if (isPoison){
            // gameObject.tag = "Obstacle"; // Set the tag of the GameObject to "Obstacle"
            Renderer renderer = gameObject.GetComponent<Renderer>(); // Get the Renderer component
            if (renderer != null) {
                renderer.material = poisonMaterial; // Change the material of the object
            }
            Collider collider = gameObject.GetComponent<Collider>(); // Get the Collider component
            if (collider != null) {
                collider.enabled = true; // Turn the collider on
            }
            
        }
    }
    
}
