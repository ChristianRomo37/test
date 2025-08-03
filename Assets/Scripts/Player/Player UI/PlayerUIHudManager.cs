using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlayerUIHudManager : MonoBehaviour
{
    [SerializeField] UI_Stat_Bar healthBar;

    [SerializeField] WeaponPanel weaponPanel;


    public void SetNewHealthValue(float oldValue, float newValue)
    {
        if(oldValue < newValue)
        {
            healthBar.lerpFrontSlider = true;
            healthBar.lerpBackSlider = false;
        }
        else if(oldValue > newValue)
        {
            healthBar.lerpBackSlider = true;
            healthBar.lerpFrontSlider = false;
        }
        healthBar.SetStat(newValue);
    }

    public void SetMaxHealthValue(float maxHealth)
    {
        healthBar.SetMaxStat(maxHealth);
    }

    public void SetAmmoText(float currentAmmo, float maxAmmo)
    {
        weaponPanel.SetCurrentAmmo(currentAmmo);
        weaponPanel.SetMaxAmmo(maxAmmo);
    }
}
