using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Stat_Bar : MonoBehaviour
{
    private Slider slider;
    private Slider backgroundSlider;
    private float t;
    protected virtual void Awake()
    {
        slider = GetComponent<Slider>();
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

    public virtual void SetStat(int newValue)
    {
        backgroundSlider.value = newValue;
        SliderLerp();
    }

    public virtual void SetMaxStat(int maxValue)
    {
        slider.maxValue = maxValue;
        backgroundSlider.maxValue = maxValue;
        backgroundSlider.value = maxValue;
        SliderLerp();
    }

    public virtual void SliderLerp()
    {
        t += Time.deltaTime * 2f;
        Mathf.Lerp(slider.value, backgroundSlider.value, t);
    }
}
