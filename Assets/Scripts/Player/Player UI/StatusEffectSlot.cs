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



    public virtual IEnumerator SetStatusEffectSlot(Image image, float timeToKeepUp)
    {
        statusEffectImage.sprite = image.sprite;

        yield return new WaitForSeconds(timeToKeepUp);

        statusEffectImage.sprite = null;
    }

    public virtual void RemoveStatusEffectSlot()
    {
        statusEffectImage.sprite = null;
    }

}
