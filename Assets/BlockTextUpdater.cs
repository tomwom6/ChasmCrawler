using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BlockTextUpdater : MonoBehaviour
{
    public TMP_Text BlockText;
    public GameObject targetGameObject;
    void Start()
    {
        PlayerHealth healthComponent = targetGameObject.GetComponent<PlayerHealth>();
        BlockText = GameObject.Find("Text (TMP)(block)").GetComponent<TextMeshProUGUI>();
        if (healthComponent != null)
        {

        bool canBlock = healthComponent.canBlock;
        BlockText.color = Color.blue;
        
        }
    }

    // Update is called once per frame
    public void UpdateBlockText(bool canBlock)
    {
        if (canBlock == false)
        {
            BlockText.color = Color.red;
        }
        else
        {
            BlockText.color = Color.blue;
        
        }
        
    }
}