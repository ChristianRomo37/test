using UnityEngine;

[CreateAssetMenu(menuName = "PoweUp Effect/Bullet Speed Modifier")]
public class BulletSpeedMod : PU_Modifer
{
    [Header("Multiplier (e.g. 2 = double bullet speed)")]
    public float speedMultiplier = 2f;

    public override void Activate(GameObject staplerObject)
    {
        FireStapler stapler = staplerObject.GetComponentInChildren<FireStapler>();
        if (stapler == null) return;

        stapler.bulletSpeed *= speedMultiplier;
    }
}