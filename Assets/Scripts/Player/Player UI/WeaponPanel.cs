using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentAmmoText;
    [SerializeField] TextMeshProUGUI maxAmmoText;


    public virtual void SetMaxAmmo(float maxAmmo)
    {
        maxAmmoText.SetText(maxAmmo.ToString());
    }

    public virtual void SetCurrentAmmo(float currentAmmo)
    {
        currentAmmoText.SetText(currentAmmo.ToString());
    }



}
