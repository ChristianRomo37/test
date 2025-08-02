using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Stat_Bar : MonoBehaviour
{
    [SerializeField] Slider frontSlider;
    [SerializeField] Slider backSlider;
    Color backSliderColorOrig;
    Color frontSliderColorOrig;
    private Image backSliderFillImage;
    private Image frontSliderFillImage;
    private float t;
    public bool lerpFrontSlider;
    public bool lerpBackSlider;
    protected virtual void Awake()
    {
        t = 0;
        backSliderFillImage = backSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>();
        frontSliderFillImage = frontSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>();
        backSliderColorOrig = backSliderFillImage.color;
        frontSliderColorOrig = frontSliderFillImage.color;
    }


    protected virtual void Update()
    {
        if(frontSlider.value != backSlider.value)
        {
           SliderLerp();
        }
        else
        {
            t = 0;
            frontSliderFillImage.color = frontSliderColorOrig;
            backSliderFillImage.color = backSliderColorOrig;
        }
    }

    public virtual void SetStat(float newValue)
    {
        if(lerpBackSlider)
        {
            frontSlider.value = newValue;
        }
        else if(lerpFrontSlider)
        {
            backSlider.value = newValue;
        }
        SliderLerp();
        
    }

    public virtual void SetMaxStat(float maxValue)
    {
        frontSlider.maxValue = maxValue;
        backSlider.maxValue = maxValue;
        backSlider.value = maxValue;
        SliderLerp();
    }

    public virtual void SliderLerp()
    {
        t += Time.deltaTime * 2f;
        if(lerpBackSlider)
        {
            backSlider.value = Mathf.Lerp(backSlider.value, frontSlider.value, t);
        }
        else if(lerpFrontSlider)
        {
            backSliderFillImage.color = Color.green;
            frontSlider.value = Mathf.Lerp(frontSlider.value, backSlider.value, t);
        }
        
        
       
    }
}
