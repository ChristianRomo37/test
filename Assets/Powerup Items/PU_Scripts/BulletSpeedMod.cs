using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp Effect/Bullet Speed Modifier")]
public class BulletSpeedMod : PU_Modifer
{
    [Header("Speed += #")]
    public float speedPlus = 50f;

    public override void Activate(GameObject staplerObject)
    {
        FireStapler stapler = staplerObject.GetComponentInChildren<FireStapler>();
        if (stapler == null) return;

        stapler.bulletSpeed += speedPlus;
    }
}