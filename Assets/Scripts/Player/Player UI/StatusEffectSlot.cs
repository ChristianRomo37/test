using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectSlot : MonoBehaviour
{

    Image statusEffectImage;


    protected virtual void Awake()
    {
        statusEffectImage = GetComponentInChildren<Image>();
    }



    public virtual void SetStatusEffectSlot(Image image)
    {
        statusEffectImage.sprite = image.sprite;
    }

    public virtual void RemoveStatusEffectSlot()
    {
        statusEffectImage.sprite = null;
    }

}
