using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - player.transform.position; 

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset; 

    }
}
