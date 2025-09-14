using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    public PlayerHealthTextUpdater PlayerHealthTextUpdater;
    public BlockTextUpdater BlockTextUpdater;
    public GameObject myPlayer;

    public float health;
    public bool MonsterCanAttack = true;

    public bool canBlock = true;
    public bool Blocking = false;
    public int AttackCooldownUpper = 6;
    public float NormalMonsterDamage = 5f;
    public GameObject Difficulty;
    public LevelStats LevelStats;
    public RayProjectile RayProjectile;
    public Monsters MonstersScript;
    public GameObject MonstersObject;
    public bool turnRed = false;
    public int timeToBlock = 5;
    public int timeForBlock = 1;
    public GameObject GameplayOverall;
    public GameObject MainMenuOverall;    
    public TMP_Text LevelText;
    public TMP_Text BlockText;
    public TMP_Text PlayerText;
    public GameObject GameOverScreen;
    public void changeHealth(float amount)
    {
        health-=amount;
        PlayerHealthTextUpdater?.UpdateHealthText(health);
        StartCoroutine(FlashRed());
    }
    void OnEnable()
    {
        canBlock = true;
        Blocking = false;
        turnRed = false;
    }
    void Start()
    {
        health = gameObject.GetComponent<PlayerInfo>().health;
        BlockTextUpdater = GetComponent<BlockTextUpdater>();
        BlockTextUpdater.UpdateBlockText(canBlock);
        PlayerHealthTextUpdater?.UpdateHealthText(health);  
        Difficulty = GameObject.Find("Difficulty");
        LevelStats = Difficulty.GetComponent<LevelStats>();
        MonstersObject = GameObject.Find("MonstersObject");
        MonstersScript = MonstersObject.GetComponent<Monsters>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && canBlock)
            {
                Block();
            }
        GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach(GameObject Monster in Monsters)
        {
            float dist = Vector2.Distance(myPlayer.transform.position, Monster.transform.position);  
            if (dist < 1.5 && MonsterCanAttack && !Blocking)
                {
                    changeHealth(NormalMonsterDamage * LevelStats.Difficulty);
                    Debug.Log("health change");
                    MonsterCanAttack = false;
                    StartCoroutine(MonsterAttackCooldown(Random.Range(2,AttackCooldownUpper)));

                }
        }   
        if (health <= 0)

        {   
            StartCoroutine(GameOver());   
        
        }
    

        
    
    }

    public IEnumerator FlashRed()
    {
        turnRed = false;
        if (Blocking == false)
        {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }
        
        
    }
    IEnumerator MonsterAttackCooldown(int seconds) 
    {
        yield return new WaitForSeconds(seconds);

        MonsterCanAttack = true;
    }
    
    IEnumerator BlockCooldown(int timeToBlockCooldown) 
    {
        yield return new WaitForSeconds(timeForBlock);
        Blocking = false;
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(timeToBlockCooldown);
        canBlock = true;
        BlockTextUpdater?.UpdateBlockText(canBlock);
        
        

    }    
    public void Block()
    {
        Blocking = true;
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        canBlock = false;
        BlockTextUpdater?.UpdateBlockText(canBlock);
        StartCoroutine(BlockCooldown(10));
    }
    
    IEnumerator GameOver()
    {
       Debug.Log("game over");
       LevelText.enabled = false;
       PlayerText.enabled = false;
       BlockText.enabled = false;
       GameOverScreen.SetActive(true);
        PlayerPrefs.SetFloat("Health", 100f);
        PlayerPrefs.SetFloat("Speed", 5f);
        PlayerPrefs.SetInt("Damage", 8 );
        PlayerPrefs.SetFloat("ProjectileShootCooldown", 2f);
        PlayerPrefs.SetFloat("ProjectileSpeed", 3f);
        PlayerPrefs.SetFloat("ProjectileDamage", 2f);
        PlayerPrefs.SetFloat("ProjectileSizeX", 0.5f);
        PlayerPrefs.SetFloat("ProjectileSizeY", 0.5f);
        PlayerPrefs.SetFloat("ProjectileSizeZ", 0.5f);
        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.SetFloat("Difficulty", 1.0f);
        GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Monster");
        GameObject[] RoomsReals = GameObject.FindGameObjectsWithTag("RoomsReal");
        GameObject[] RangedMonsters = GameObject.FindGameObjectsWithTag("RangedMonster");
        GameObject[] HomingRangedMonsters = GameObject.FindGameObjectsWithTag("HomingRangedMonster");
        GameObject[] Projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach(GameObject Monster in Monsters)
        {
            Destroy(Monster);
        }
        foreach(GameObject RoomsReal in RoomsReals)
        {
            Destroy(RoomsReal);
        }
        foreach(GameObject RangedMonster in RangedMonsters)
        {
            Destroy(RangedMonster);
        }
        foreach(GameObject HomingRangedMonster in HomingRangedMonsters)
        {
            Destroy(HomingRangedMonster);
        }
        foreach(GameObject Projectile in Projectiles)
        {
            Destroy(Projectile);
        }
        
        PlayerPrefs.Save();
        health = PlayerPrefs.GetFloat("Health");
        gameObject.GetComponent<PlayerInfo>().health = PlayerPrefs.GetFloat("Health");
        gameObject.GetComponent<PlayerMovement>().moveSpeed = PlayerPrefs.GetFloat("Speed");
        gameObject.GetComponent<PlayerInfo>().playerDamage = PlayerPrefs.GetInt("Damage");
        gameObject.GetComponent<PlayerShoot>().playerShootCooldown = PlayerPrefs.GetFloat("ProjectileShootCooldown");
        gameObject.GetComponent<PlayerShoot>().speed = PlayerPrefs.GetFloat("ProjectileSpeed");
        gameObject.GetComponent<PlayerShoot>().ProjectileDamage = PlayerPrefs.GetFloat("ProjectileDamage");
        gameObject.GetComponent<PlayerShoot>().ProjectileSize.x = PlayerPrefs.GetFloat("ProjectileSizeX");
        gameObject.GetComponent<PlayerShoot>().ProjectileSize.y = PlayerPrefs.GetFloat("ProjectileSizeY");
        gameObject.GetComponent<PlayerShoot>().ProjectileSize.z = PlayerPrefs.GetFloat("ProjectileSizeZ");
        Difficulty.GetComponent<LevelStats>().Level = PlayerPrefs.GetInt("Level");
        Difficulty.GetComponent<LevelStats>().Difficulty = PlayerPrefs.GetFloat("Difficulty");
        GameObject.Find("Staircase").GetComponent<StaircaseController>().LevelChanged = true;
        GameObject.Find("Staircase").GetComponent<StaircaseController>().MoveStaircaseToPlayer();  
        yield return new WaitForSeconds(5.0f); 
        GameObject.Find("Staircase").GetComponent<StaircaseController>().LevelChangeCoroutine();
        yield return new WaitForSeconds(5.0f);
     
        MainMenuOverall.SetActive(true);
        LevelText.enabled = true;
        PlayerText.enabled = true;
        BlockText.enabled = true;
        GameOverScreen.SetActive(false);
        PlayerHealthTextUpdater?.UpdateHealthText(health);
        GameplayOverall.SetActive(false);   
    
        

    } 
}