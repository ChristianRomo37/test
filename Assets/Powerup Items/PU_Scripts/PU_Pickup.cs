using Unity.VisualScripting;
using UnityEngine;

public class PU_Pickup : MonoBehaviour
{
    public PU_Modifer pu_modifier;

    private void OnTriggerEnter(Collider other)
    {
        var playerRB = other.GetComponentInParent<Rigidbody>();

        // Check playerRB filled
        if (playerRB != null)
        {
            ActivatePowerUp(playerRB);
        }

    }

    // Update is called once per frame
    void ActivatePowerUp(Rigidbody playerRB)
    {
        Debug.Log("PowerUp PickedUp");

        // Give effect to player
        var activate = playerRB.GetComponent<PlayerMovement>();
        activate.ApplyPowerUpMod(pu_modifier);

        // Destroy Object
        Destroy(gameObject);
    }
}
