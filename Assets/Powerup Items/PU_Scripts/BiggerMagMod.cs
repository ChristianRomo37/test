using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(menuName = "PowerUp Effect/Bigger Mag Modifer")]

public class BiggerMagMod : PU_Modifer
{
    [SerializeField] public int addAmmo;

    public override void Activate(GameObject target)
    {
        var stapler = target.GetComponentInChildren<FireStapler>();

        stapler.maxMagazine += addAmmo;
        stapler.currMag = stapler.maxMagazine;
    }

}
