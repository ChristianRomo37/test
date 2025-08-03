using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectSlot : MonoBehaviour
{

    [SerializeField] Image statusQuickEffectImage;

    [SerializeField] Image[] statusStoredEffectImage;




    public virtual IEnumerator SetStatusQuickEffectSlot(Image image, float timeToKeepUp)
    {
        statusQuickEffectImage.gameObject.SetActive(true);
        statusQuickEffectImage.sprite = image.sprite;

        yield return new WaitForSeconds(timeToKeepUp);


        statusQuickEffectImage.sprite = null;
        statusQuickEffectImage.gameObject.SetActive(false);
    }

    public virtual void RemoveStatusQuickEffect()
    {
        statusQuickEffectImage.sprite = null;
        statusQuickEffectImage.gameObject.SetActive(false);
    }

    public virtual void SetStatusStoredEffect(Image image)
    {
        for(int i = 0; i < statusStoredEffectImage.Length; i++)
        {
            if(statusStoredEffectImage[i] != null)
            {
                if (statusStoredEffectImage[i] == image)
                {
                    continue;
                }
                else
                {
                    statusStoredEffectImage[i].gameObject.SetActive(true);
                    statusStoredEffectImage[i] = image;
                    return;
                }
            }
        }
    }

    public virtual void ClearStatusEffectList()
    {
        for(int i = 0; i < statusStoredEffectImage.Length; i++)
        {
            statusStoredEffectImage[i] = null;
            statusStoredEffectImage[i].gameObject.SetActive(false);
        }
    }





}
