using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp Effect/Pierce Bullets Modifer")]
public class PierceBulletsMod : PU_Modifer
{
    public bool pierceBullets = false;

    public void Start()
    {
        pierceBullets = false;
    }

    public override void Activate(GameObject target)
    {
        pierceBullets = true;
    }
}