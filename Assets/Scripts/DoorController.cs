using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject instructions;
    public Animator anim;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Door")
        {
            instructions.SetActive(true);
            anim = other.GetComponentInChildren<Animator>();
            PlayerInputController.instance.canInteract = true;
            if (PlayerInputController.instance.Interact) 
            {
                anim.SetTrigger("CloseOpen");
                PlayerInputController.instance.Interact = false;
                PlayerInputController.instance.isInteracting = false;
            }
        }
;    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Door")
        {
            PlayerInputController.instance.canInteract = false;
            instructions.SetActive(false);
        }
    }
}
