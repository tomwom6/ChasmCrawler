using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthTextUpdater : MonoBehaviour
{
    private float health;
    public TMP_Text text;
    public GameObject targetGameObject;
    public GameObject myPlayer;
    public PlayerHealth healthComponent;
    
    void Start()
    {
        myPlayer = GameObject.Find("Player");
        healthComponent = myPlayer.GetComponent<PlayerHealth>();


        if (healthComponent != null)
        {

            health = healthComponent.health;
            
            Application.Quit();

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

