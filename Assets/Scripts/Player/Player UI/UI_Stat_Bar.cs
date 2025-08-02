using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Stat_Bar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Slider backgroundSlider;
    private float t;
    protected virtual void Awake()
    {
        t = 0;
    }

    protected virtual void Update()
    {
        if(slider.value != backgroundSlider.value)
        {
           SliderLerp();
        }
        else
        {
            t = 0;
        }
    }

    public virtual void SetStat(float newValue)
    {
        backgroundSlider.value = newValue;
        SliderLerp();
    }

    public virtual void SetMaxStat(float maxValue)
    {
        slider.maxValue = maxValue;
        backgroundSlider.maxValue = maxValue;
        backgroundSlider.value = maxValue;
        SliderLerp();
    }

    public virtual void SliderLerp()
    {
        t += Time.deltaTime * 2f;
        slider.value = Mathf.Lerp(slider.value, backgroundSlider.value, t);
       
    }
}
