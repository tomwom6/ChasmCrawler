using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaircaseController : MonoBehaviour
{
    public RoomTemplates RoomTemplates;
    public GameObject RoomTemplatesObject;
    public GameObject myPlayer;
    public List<GameObject> Rooms;
    public float dist;
    public GameObject StartRoom;
    public GameObject BossRoom;
    public bool LevelChanged = false;
    public LevelStats LevelStats;
    public GameObject DifficultyObject;
    public float TempSpeed;
    public PlayerMovement PlayerMovement;
    public GameObject Boss1;
    public GameObject MonstersObject;
    public bool Boss1Status = false;
    public bool Boss1HasSpawned = false;
    public GameObject InstantiatedBoss1;
    public bool LevelChanging = false;

    void OnEnable()
    {
        LevelChanging = false;
    }
    void Start()
    {
        LevelChanging = false;
        MonstersObject = GameObject.Find("MonstersObject");
        myPlayer = GameObject.Find("Player");
        RoomTemplatesObject = GameObject.Find("RoomTemplatesObject");
        RoomTemplates = RoomTemplatesObject.GetComponent<RoomTemplates>();
        Invoke("MoveStaircaseToRoom", 2.0f);
        DifficultyObject = GameObject.Find("Difficulty");
        PlayerMovement = myPlayer.GetComponent<PlayerMovement>();
        LevelStats = DifficultyObject.GetComponent<LevelStats>();
        
        

    }

    // Update is called once per frame
    public void MoveStaircaseToRoom()
    {
        gameObject.transform.position = RoomTemplates.Rooms[(RoomTemplates.Rooms.Count)-1].transform.position;
    }
    public void MoveStaircaseToPlayer()
    {
        gameObject.transform.position = myPlayer.transform.position;
    }

    IEnumerator LevelChange()
    {
        LevelChanging = true;
        LevelStats.Level = LevelStats.Level + 1;
        LevelStats.Difficulty = LevelStats.Difficulty + 0.5f;
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
        RoomTemplates.SpawnID = 0;
        RoomTemplates.Rooms.Clear();
        TempSpeed = PlayerMovement.moveSpeed;   
        PlayerMovement.moveSpeed = 0f;
        yield return new WaitForSeconds(2.0f);
        LevelChanged = false;
        
        if (LevelStats.Level % 3 == 0)
        {
            Instantiate(BossRoom, myPlayer.transform.position, transform.rotation);
            yield return new WaitForSeconds(2.0f);
            PlayerMovement.moveSpeed = TempSpeed;
            gameObject.transform.position = new Vector3(10000,0,0);
            yield return new WaitForSeconds(2.0f);
            InstantiatedBoss1 = Instantiate(Boss1, myPlayer.transform.position, transform.rotation);
            Boss1HasSpawned = true;
            Boss1Status = true;
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(2.0f);
                Instantiate(MonstersObject.GetComponent<Monsters>().Monster1, InstantiatedBoss1.transform.position, transform.rotation);
                yield return new WaitForSeconds(2.0f);
                Instantiate(MonstersObject.GetComponent<Monsters>().RangedMonsterHoming, InstantiatedBoss1.transform.position, transform.rotation);
            }
        

            
        }
    

        else{
            Instantiate(StartRoom, transform.position, transform.rotation);

            yield return new WaitForSeconds(4.0f);
            PlayerMovement.moveSpeed = TempSpeed;
            MoveStaircaseToRoom();
            Debug.Log(LevelStats.Difficulty);
        
        }
        LevelChanging = false;      
        
    }
   public void LevelChangeCoroutine()
    {
        
        StartCoroutine(LevelChange());
    }
    void Update()
    {

        if (InstantiatedBoss1 == null)
        {
            Boss1Status = false;
        }
        if (myPlayer != null)
        {
        if ((LevelStats.Level % 3 == 0) && Boss1Status == false && Boss1HasSpawned == true)
            {
                MoveStaircaseToPlayer();
                Boss1Status = false;
                Boss1HasSpawned = false;
               
            }
        }
        dist = Vector2.Distance(myPlayer.transform.position, gameObject.transform.position);  
        if ((dist < 1.5) && (Input.GetKeyDown(KeyCode.E)) && (LevelChanging == false))
        {
            
            LevelChanged = true;
            StartCoroutine(LevelChange());
        }
        
    }
    
}