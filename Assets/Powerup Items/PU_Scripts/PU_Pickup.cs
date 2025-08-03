using Unity.VisualScripting;
using UnityEngine;

public class PU_Pickup : MonoBehaviour
{
    public PU_Modifer pu_modifier;
    public AudioClip clip;
    public AudioSource source;

    private void OnTriggerEnter(Collider other)
    {
        var playerRB = other.GetComponentInParent<Rigidbody>();

        // Check playerRB filled
        if (playerRB != null && !playerRB.CompareTag("Bullet"))
        {
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
        activate.StorePowerUp(pu_modifier);

        // Destroy Object
        Destroy(gameObject);
    }
}
