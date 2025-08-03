using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StausEffectManager : MonoBehaviour
{

    [SerializeField] StatusEffectSlot[] statusEffectSlots;



    public void enableStatusEffectSlot()
    {
        for(int i = 0; i < statusEffectSlots.Length; i++)
        {
            if(!statusEffectSlots[i].gameObject.activeSelf)
            {
                statusEffectSlots[i].gameObject.SetActive(true);
                
            }
        }
    }

    
}
