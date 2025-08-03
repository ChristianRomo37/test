using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectSlot : MonoBehaviour
{

    [SerializeField] Image statusQuickEffectImage;

    [SerializeField] Image[] statusStoredEffectImage;




    public virtual IEnumerator SetStatusQuickEffectSlot(Sprite image, float timeToKeepUp)
    {
        statusQuickEffectImage.gameObject.SetActive(true);
        statusQuickEffectImage.sprite = image;

        yield return new WaitForSeconds(timeToKeepUp);


        statusQuickEffectImage.sprite = null;
        statusQuickEffectImage.gameObject.SetActive(false);
    }

    public virtual void RemoveStatusQuickEffect()
    {
        statusQuickEffectImage.sprite = null;
        statusQuickEffectImage.gameObject.SetActive(false);
    }

    public virtual void SetStatusStoredEffect(Sprite image)
    {
        for(int i = 0; i < statusStoredEffectImage.Length; i++)
        {
            if(statusStoredEffectImage[i].sprite != null)
            {
                if (statusStoredEffectImage[i].sprite == image)
                {
                    continue;
                }
            }
            else
            {
                statusStoredEffectImage[i].gameObject.SetActive(true);
                statusStoredEffectImage[i].sprite = image;
                return;
            }
        }
    }

    public virtual void ClearStatusEffectList()
    {
        for(int i = 0; i < statusStoredEffectImage.Length; i++)
        {
            statusStoredEffectImage[i].gameObject.SetActive(false);
            statusStoredEffectImage[i] = null;
        }
    }





}
