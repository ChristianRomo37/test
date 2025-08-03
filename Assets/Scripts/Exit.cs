using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.LightTransport;

public class Exit : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Scene CurrentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(CurrentScene.name);
        }
;
    }
}
