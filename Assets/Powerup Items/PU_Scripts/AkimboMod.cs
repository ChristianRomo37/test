using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp Effect/Akimbo Modifier")]

public class AkimboMod : PU_Modifer
{
    public override void Activate(GameObject target)
    {
        GameManager.instance.akimboArm.SetActive(true);
    }

}
        