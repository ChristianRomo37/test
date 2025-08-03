using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHudManager : MonoBehaviour
{
    [SerializeField] UI_Stat_Bar healthBar;

    [SerializeField] WeaponPanel weaponPanel;

    [SerializeField] StatusEffectSlot statusEffectSlot;

    [SerializeField] public ReticleSpread reticleSpread;


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

    public void SetStatusEffectSlot(Image image)
    {
        if(!statusEffectSlot.gameObject.activeSelf)
        {
            statusEffectSlot.gameObject.SetActive(true);
        }
        statusEffectSlot.SetStatusEffectSlot(image);
    }

    public void RemoveStatusEffectSlot()
    {
        statusEffectSlot.gameObject.SetActive(false);
        statusEffectSlot.RemoveStatusEffectSlot();
    }

    public void SpreadReticleIsShooting(bool isShooting)
    {
        reticleSpread.isShooting = isShooting;
    }
}
