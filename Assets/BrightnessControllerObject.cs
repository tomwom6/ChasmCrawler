using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessControllerObject : MonoBehaviour
{
    // Start is called before the first frame update
        void OnEnable()
    {
        float brightness = PlayerPrefs.GetFloat("Brightness", 1f);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = brightness;
        spriteRenderer.color = color;
    }
}
