using UnityEngine;
using UnityEngine.LightTransport;

public class Exit : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            WorldSaveGameManager.instance.LoadWorldScene();
        }
;
    }
}
