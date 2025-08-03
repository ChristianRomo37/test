using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp Effect/Health Modifer")]
public class HealthMod : PU_Modifer
{
    public int HpValue;

    // + player currHp by HpValue
    public override void Activate(GameObject target)
    {
        var playerHealth = target.GetComponent<PlayerHealth>();

        Debug.Log("Curr HP: " + playerHealth.currHp);

        playerHealth.currHp += HpValue;
        playerHealth.currHp = Mathf.Clamp(playerHealth.currHp, 0, playerHealth.MaxHp);
        PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue(playerHealth.currHp, playerHealth.currHp);

        Debug.Log("Curr HP: " + playerHealth.currHp);
    }
}
