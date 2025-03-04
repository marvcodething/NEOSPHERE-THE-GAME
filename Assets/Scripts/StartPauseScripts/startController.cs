using UnityEngine;
using UnityEngine.SceneManagement;
public class startController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }

}
