using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsEffect : MonoBehaviour
{
    
    public float healthIncrease = 0f;
    public int damageIncrease = 0;
    public float speedIncrease = 0f;
    public float fireRateIncrease = 0f;
    public float projectileSpeedIncrease = 0f;
    public Vector3 projectileSizeIncrease = new Vector3(0, 0, 0);
    public int projectileDamageIncrease = 0;
    void Update()
    {
        
        if (Input.GetKeyDown("e") && Vector2.Distance(GameObject.Find("Player").transform.position, transform.position)  < 1.5f)
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().health += healthIncrease;
            GameObject.Find("Player").GetComponent<PlayerInfo>().playerDamage += damageIncrease;
            GameObject.Find("Player").GetComponent<PlayerMovement>().moveSpeed += speedIncrease;
            if (GameObject.Find("Player").GetComponent<PlayerShoot>().playerShootCooldown - fireRateIncrease > 0.1f)
            {
                    GameObject.Find("Player").GetComponent<PlayerShoot>().playerShootCooldown -= fireRateIncrease;
            }
            GameObject.Find("Player").GetComponent<PlayerShoot>().speed += projectileSpeedIncrease;
        
            GameObject.Find("Player").GetComponent<PlayerShoot>().ProjectileSize += projectileSizeIncrease;
            GameObject.Find("Player").GetComponent<PlayerShoot>().ProjectileDamage += projectileDamageIncrease;
            GameObject.Find("Player").GetComponent<PlayerHealthTextUpdater>().UpdateHealthText(GameObject.Find("Player").GetComponent<PlayerHealth>().health);
            Destroy(gameObject);
        }
    }

}
