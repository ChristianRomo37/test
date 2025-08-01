using UnityEngine;

[CreateAssetMenu(menuName = "PoweUp Effect/Health Modifer")]

public class HealthMod : PU_Modifer
{
    public int HpValue;

    // + player currHp by HpValue
    public override void Activate(GameObject target)
    {
        var playerhHealth = target.GetComponent<PlayerHealth>();

        Debug.Log(playerhHealth.currHp);

        playerhHealth.currHp += HpValue;

        Debug.Log(playerhHealth.currHp);
    }
}
