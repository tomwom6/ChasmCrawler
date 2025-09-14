using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayProjectileHoming : MonoBehaviour
{
    public GameObject myPlayer;
    public Rigidbody myRigidBody;
    private LayerMask myLayerMask;
    private LayerMask myLayerMask1;
    private LayerMask myLayerMask2;
    private float distFromPlayer;
    public PlayerHealth playerHealth;
    public float speed = 4;    
    public bool canHit = true;                                        
    public new  Vector3 playerPosition;                                                                                                                                
    public new Vector3 moveTo;
    public GameObject Difficulty;
    public LevelStats LevelStats;
    void Start()
    {
        myPlayer = GameObject.Find("Player");
        playerHealth = myPlayer.GetComponent<PlayerHealth>();
        myLayerMask1 = LayerMask.GetMask("ExcludeRaycast");
        LevelStats = GameObject.Find("Difficulty").GetComponent<LevelStats>();
        myLayerMask2 = LayerMask.GetMask("Monsters");
        myLayerMask = myLayerMask1 | myLayerMask2;
        CircleCollider2D bc;
        bc = gameObject.GetComponent<CircleCollider2D>();
        PolygonCollider2D Playerbc;
        Playerbc = myPlayer.GetComponent<PolygonCollider2D>();
        playerPosition = myPlayer.transform.position;
        moveTo = myPlayer.transform.position - transform.position;
        moveTo = moveTo * 10000;
    
    }

        
    void Update()
    {
        if (myPlayer != null)
        {

            StartCoroutine(DieIn5Seconds());
            transform.position = Vector2.MoveTowards(transform.position, myPlayer.transform.position, speed * Time.deltaTime);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (gameObject != null)
        {
        if (myPlayer != null && playerHealth != null)
        {
            if (col.gameObject.tag != ("RangedMonster") && col.gameObject.tag != ("HomingRangedMonster") && col.gameObject.tag != ("Monster") && col.gameObject.tag != ("SpawnPoint"))
            {
                if (col.gameObject.name == ("Player"))
                {
                    if (canHit == true && playerHealth.Blocking == false)
                    {
                        playerHealth.changeHealth(3.5f * LevelStats.Difficulty);
                        canHit = false;
                        playerHealth.turnRed = true;

                    }

                    

                    
                }
                Destroy(gameObject);
                
            }
            
        }
        }
        
    }
    IEnumerator DieIn5Seconds()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
