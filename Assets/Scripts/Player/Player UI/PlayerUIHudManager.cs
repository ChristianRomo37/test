using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlayerUIHudManager : MonoBehaviour
{
    [SerializeField] UI_Stat_Bar healthBar;

    public void SetNewHealthValue(float oldValue, float newValue)
    {
        healthBar.SetStat(newValue);
    }

    public void SetMaxHealthValue(float maxHealth)
    {
        healthBar.SetMaxStat(maxHealth);
    }
}
