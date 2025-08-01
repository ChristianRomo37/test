using UnityEngine;

[CreateAssetMenu(menuName = "PoweUp Effect/Speed Modifer")]
public class SpeedMod : TimedPU_Modiifer
{
    public float speedValue;

    public override void Activate(GameObject target)
    {
        var playerMovement = target.GetComponent<PlayerMovement>();

        Debug.Log("Speed Before PU Activate: " + playerMovement.origSpeed + " & " + playerMovement.sprintSpeed);

        playerMovement.origSpeed += speedValue;
        playerMovement.sprintSpeed += speedValue;

        Debug.Log("Speed PU Activated: " + playerMovement.origSpeed + " & " + playerMovement.sprintSpeed);
    }

    public override void Deactivate(GameObject target)
    {
        var playerMovement = target.GetComponent<PlayerMovement>();

        playerMovement.origSpeed -= speedValue;
        playerMovement.sprintSpeed -= speedValue;

        Debug.Log("Speed PU Deactivated: " + playerMovement.origSpeed + " & " + playerMovement.sprintSpeed);
    }
}
