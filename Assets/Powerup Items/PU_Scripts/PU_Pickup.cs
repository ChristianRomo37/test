using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class PU_Pickup : MonoBehaviour
{
    [SerializeField] Sprite image;
    public PU_Modifer pu_modifier;
    public AudioClip clip;
    public AudioSource source;

    private void OnTriggerEnter(Collider other)
    {
        var playerRB = other.GetComponentInParent<Rigidbody>();

        // Check playerRB filled
        if (playerRB != null && !playerRB.CompareTag("Bullet") && !playerRB.CompareTag("Enemy Bullet"))
        {
            //if (gameObject.CompareTag("Time Reverse"))
            //    GameManager.instance.rewindManager.enabled = true;

            ActivatePowerUp(playerRB);
        }

    }

    // Update is called once per frame
    void ActivatePowerUp(Rigidbody playerRB)
    {
        Debug.Log("PowerUp PickedUp");

        AudioSource.PlayClipAtPoint(clip, transform.position);

        // Give effect to player
        var activate = playerRB.GetComponent<PlayerMovement>();
        
        if (gameObject.CompareTag("Stored")) //|| gameObject.CompareTag("Time Reverse"))
        {
            PlayerUIManager.instance.playerUIHudManager.SetStatusStoredEffectSlot(image);
            activate.StorePowerUp(pu_modifier);
        }
        else
        {
            PlayerUIManager.instance.playerUIHudManager.SetStatusQuickEffectSlot(image, 2);
            activate.ApplyPowerUpMod(pu_modifier);
        }


        // Destroy Object
        Destroy(gameObject);
    }
}
