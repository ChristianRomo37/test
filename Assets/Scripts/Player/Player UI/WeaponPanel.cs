using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentAmmoText;
    [SerializeField] TextMeshProUGUI maxAmmoText;


    public void SetMaxAmmo(float maxAmmo)
    {
        maxAmmoText.SetText(maxAmmo.ToString());
    }

    public void SetCurrentAmmo(float currentAmmo)
    {
        currentAmmoText.SetText(currentAmmo.ToString());
    }



}
