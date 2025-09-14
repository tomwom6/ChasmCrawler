using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthTextUpdater : MonoBehaviour
{   
    private float health;
    public TMP_Text text;
    public GameObject targetGameObject;
    public PlayerInfo PlayerInfo;
    void Start()
    {
        PlayerInfo = GetComponent<PlayerInfo>();
        PlayerHealth healthComponent = targetGameObject.GetComponent<PlayerHealth>();
        text.text = (health).ToString();

        if (healthComponent != null)
        {

            health = PlayerInfo.health;
            

        }
    }

    // Update is called once per frame
    
    void Update()
    {
        text.text = (health).ToString();

        // Update the health text box based on the current health value
    }
    public void UpdateHealthText(float newHealth)
    {
        health = newHealth;
        text.text = health.ToString();
    }
}

