using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[CreateAssetMenu(menuName = "PowerUp Effect/Max HP Modifer")]

public class MaxHPMod : PU_Modifer
{
    public int HpValue;

    public override void Activate(GameObject target)
    {
        var playerHealth = target.GetComponent<PlayerHealth>();

        Debug.Log("Max HP: " + playerHealth.MaxHp);

        playerHealth.MaxHp += HpValue;
        playerHealth.currHp = playerHealth.MaxHp;
        PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue(playerHealth.currHp, playerHealth.MaxHp);

        Debug.Log("Max HP: " + playerHealth.MaxHp);
    }
}
