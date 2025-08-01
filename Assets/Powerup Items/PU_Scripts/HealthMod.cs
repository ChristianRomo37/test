using UnityEngine;

[CreateAssetMenu(menuName = "PoweUp Effect/Health Modifer")]
public class HealthMod : PU_Modifer
{
    public int HpValue;

    // + player currHp by HpValue
    public override void Activate(GameObject target)
    {
        var playerHealth = target.GetComponent<PlayerHealth>();

        Debug.Log(playerHealth.currHp);

        playerHealth.currHp += HpValue;

        Debug.Log(playerHealth.currHp);
    }
}
