using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour

{
    // Start is called before the first frame update
    public int maxHealth = 150;
    public float health = 100f;
    public MonsterHealthTextUpdater MonsterHealthTextUpdater;
    public GameObject myPlayer;
    public GameObject Monster;
    public bool canAttack = true;
    public PlayerInfo PlayerInfo;
    public float maxDist = 1.5f;
    public GameObject ItemsObject;
    public bool AttackingMonster = false;
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public void changeHealth(float amount)
    {
        health-=amount;
        MonsterHealthTextUpdater?.UpdateHealthText(health);
        FlashRed();
    }
    
    void Start()
    {
        myPlayer = GameObject.FindWithTag("Player");
        PlayerInfo = myPlayer.GetComponent<PlayerInfo>();
        MonsterHealthTextUpdater?.UpdateHealthText(health);
        ItemsObject = GameObject.Find("ItemsObject");
    }

    // Update is called once per frame
    void Update()
    {
        if (myPlayer != null) 
        {
            float dist = Vector2.Distance(myPlayer.transform.position, Monster.transform.position);  
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dist < maxDist  && canAttack)
                {
                    MonsterDamage(Random.Range((PlayerInfo.playerDamage)-2,(PlayerInfo.playerDamage)+2 ));  
                }
            }
            if (health <= 0)
            {   
                
                int RandomDrop = Random.Range(1,16);
                if (RandomDrop == 1)
                {
                    Instantiate(ItemsObject.GetComponent<Items>().HealthPotion, transform.position, transform.rotation);
                }
                if (RandomDrop == 2)
                {
                    Instantiate(ItemsObject.GetComponent<Items>().DamagePotion, transform.position, transform.rotation);
                }
                if (RandomDrop == 3)
                {
                    Instantiate(ItemsObject.GetComponent<Items>().SpeedPotion, transform.position, transform.rotation);
                }
                if (RandomDrop == 4)
                {
                    Instantiate(ItemsObject.GetComponent<Items>().FireRatePotion, transform.position, transform.rotation);
                }
                if (RandomDrop == 5)
                {
                    Instantiate(ItemsObject.GetComponent<Items>().ProjectileSpeedPotion, transform.position, transform.rotation);
                }
                if (RandomDrop == 6)
                {
                    Instantiate(ItemsObject.GetComponent<Items>().ProjectileSizePotion, transform.position, transform.rotation);
                }
                if (RandomDrop == 7)
                {
                    Instantiate(ItemsObject.GetComponent<Items>().ProjectileDamagePotion, transform.position, transform.rotation);
                }
                if (gameObject.name == "Boss")
                {
                    Instantiate(ItemsObject.GetComponent<Items>().HealthPotion, transform.position, transform.rotation);
                    Instantiate(ItemsObject.GetComponent<Items>().FireRatePotion, transform.position, transform.rotation);

                }
                Destroy(gameObject);
            }
        }
   
    }

    public void MonsterDamage(int monsterdp)
    {

        changeHealth(monsterdp);
        StartCoroutine(FlashRed());
        canAttack = false;
        StartCoroutine(AttackCooldown(2)); 
    }
    IEnumerator FlashRed()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }
    IEnumerator AttackCooldown(int seconds) 
    {
        AttackingMonster = true;
        yield return new WaitForSeconds(seconds);

        canAttack = true;
        AttackingMonster = false;
    }      
}