using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp Effect/Reverse Time Modifer")]

public class ReverseTimeMod : PU_Modifer
{
    public override void Activate(GameObject target)
    {
        //GameManager.instance.rewindManager.rewindNow = true;
    }
}