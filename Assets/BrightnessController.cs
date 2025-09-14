using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{
    public Slider BrightnessSlider;

    void Start()
    {
        BrightnessSlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>();

        if (BrightnessSlider == null)
        {
            Debug.LogError("BrightnessSlider not found");
            return;
        }

        BrightnessSlider.onValueChanged.AddListener(ChangeBrightness);
    }

    public void ChangeBrightness(float value)
    {
        PlayerPrefs.SetFloat("Brightness", value);
        Debug.Log("Brightness set to " + value);
    }
}