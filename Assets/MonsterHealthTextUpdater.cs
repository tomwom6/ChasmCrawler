using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterHealthTextUpdater : MonoBehaviour
{
    private float health;
    public TMP_Text text;
    public GameObject targetGameObject;

    
    void Start()
    {
        
        MonsterHealth healthComponent = targetGameObject.GetComponent<MonsterHealth>();
        

        if (healthComponent != null)
        {

            health = healthComponent.health;

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

