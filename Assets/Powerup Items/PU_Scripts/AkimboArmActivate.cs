using Unity.VisualScripting;
using UnityEngine;

public class AkimboArmActivate : MonoBehaviour
{
    public void Start()
    {
        GameManager.instance.akimboArm = this.gameObject;
        GameManager.instance.akimboArm.SetActive(false);     
    }


}
