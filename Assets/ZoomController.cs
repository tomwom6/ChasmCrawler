using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomController : MonoBehaviour
{
    public Slider ZoomSlider;

    void Start()
    {
        ZoomSlider = GameObject.Find("ZoomSlider").GetComponent<Slider>();

        if (ZoomSlider == null)
        {
            Debug.LogError("ZoomSlider not found");
            return;
        }

        ZoomSlider.onValueChanged.AddListener(ChangeZoom);
    }

    public void ChangeZoom(float value)
    {
        PlayerPrefs.SetFloat("Zoom", value);
        Debug.Log("Zoom set to " + value);
        PlayerPrefs.Save();
    }
}
