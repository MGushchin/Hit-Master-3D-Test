using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public float Smooth = 5;
    public Slider Slider;

    private void Start()
    {
        Slider.minValue = 0;
        Slider.maxValue = 1;
        Slider.value = 1;
    }


    public void SetSlider(float currentValue, float maximumValue)
    {
        Slider.maxValue = maximumValue;
        Slider.value = currentValue;
    }
}
