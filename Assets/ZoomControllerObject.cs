using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomControllerObject : MonoBehaviour
{
    void OnEnable()
    {
        float zoom = PlayerPrefs.GetFloat("Zoom", 1f);
        if (zoom == 0f)
        {
            zoom = 0.1f;
        }
        GameObject camera = GameObject.Find("MainCameraGameplay"); 
        camera.GetComponent<Camera>().orthographicSize = (float)(8.288471 * zoom);
        GameObject LevelText = GameObject.Find("Text (TMP)(level)");
        GameObject BlockSprite = GameObject.Find("Text (TMP)(block)");
        LevelText.GetComponent<RectTransform>().localScale = new Vector3 (zoom, zoom, zoom);
        BlockSprite.GetComponent<RectTransform>().localScale = new Vector3 (zoom, zoom, zoom);
        

        
        
    
        

        
    }
}
