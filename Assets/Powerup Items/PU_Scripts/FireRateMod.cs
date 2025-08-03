using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp Effect/Fire Rate Modifer")]

public class FireRateMod : PU_Modifer
{
    [SerializeField] public float increaseRate;
    public float currRate;

    public override void Activate(GameObject target)
    {
        var stapler = target.GetComponentsInChildren<FireStapler>();

        stapler[0].fireRate += increaseRate;
        currRate = stapler[0].fireRate;

        if (GameManager.instance.akimboArm.activeSelf)
        { 
            stapler[1].fireRate = currRate;
            stapler[1].fireRate += increaseRate;
        }
    }
}
