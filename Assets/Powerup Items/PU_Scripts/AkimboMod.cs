using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp Effect/Akimbo Modifier")]

public class AkimboMod : PU_Modifer
{
    public FireRateMod fiReRate;

    public override void Activate(GameObject target)
    {
        var stapler = target.GetComponentInChildren<FireStapler>();
        

        //stapler.fireRate = fiReRate.currRate;

        GameManager.instance.akimboArm.SetActive(true);
    }

}
        