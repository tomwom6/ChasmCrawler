using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{
    // Start is called before the first frame update
   
    public Vector3 ShootDirection;
    public PlayerShoot PlayerShoot;
    public GameObject myPlayer;
    public Rigidbody rb;
    public float speed;
    public float timeBeforeDie;
    public float ProjectileDamage;
    void Start()

    {
        BoxCollider2D bc;
        bc = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        bc.size = new Vector2(1.3f, 1.3f);
        bc.isTrigger = true;
        myPlayer = GameObject.Find("Player");
        
        PlayerShoot= myPlayer.GetComponent<PlayerShoot>();
        Debug.Log(PlayerShoot.mousePos);
        ShootDirection = (PlayerShoot.mousePos - transform.position);
        ShootDirection = ShootDirection * 1000;
        
            
        
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, ShootDirection, speed * Time.deltaTime);
        timeBeforeDie -= Time.deltaTime;
        if (timeBeforeDie <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collided with: " + col.gameObject.name); // Add debug log statement to verify if the method is being called

        if (col.gameObject.tag == "Monster" || col.gameObject.tag == "RangedMonster" || col.gameObject.tag == "HomingRangedMonster")
        {
            MonsterHealth monsterHealth = col.gameObject.GetComponent<MonsterHealth>();
            if (monsterHealth != null)
            {
                monsterHealth.changeHealth(ProjectileDamage);
                Debug.Log("projectile destroyed 1");
                Destroy(gameObject);
            }
        }
        if(col.gameObject.name != "Player" && col.gameObject.tag != "Projectile" && col.gameObject.tag != "SpawnPoint")
        {
            Destroy(gameObject);

        }
    }
    
}
