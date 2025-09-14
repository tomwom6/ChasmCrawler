
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayerRay : MonoBehaviour

{
    public GameObject myPlayer;
    private float distFromPlayer;
    public GameObject gameObjectForMove;
    public float speed = 1;
    public Vector2 lastSeenPos;


    public Rigidbody2D myRigidBody;
    private LayerMask myLayerMask;
    private LayerMask myLayerMask1;
    private LayerMask myLayerMask2;
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public Sprite leftSprite; // Sprite for left movement
    public Sprite rightSprite; // Sprite for right movement


    void Start()
    {
        myPlayer = GameObject.FindWithTag("Player");
        myLayerMask1 = LayerMask.GetMask("ExcludeRaycast");
        myLayerMask2 = LayerMask.GetMask("Monsters");
        myLayerMask = myLayerMask1 | myLayerMask2;
        myRigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        lastSeenPos = gameObject.transform.position;

               //Exclude layer 9
    }

    void Update()
    {   
        if (myRigidBody.velocity.x < 0)
        {
            spriteRenderer.sprite = leftSprite;
        }
        else if (myRigidBody.velocity.x > 0)
        {
            spriteRenderer.sprite = rightSprite;
        }


        
        if (myPlayer != null)
        {

            Vector2 direction = myPlayer.transform.position - transform.position;
            RaycastHit2D[] hits = (Physics2D.RaycastAll(transform.position, direction, 8f, ~myLayerMask));

            if (hits.Length>0)
            {
                if (hits[0].collider.gameObject.name == ("Player"))
                
                {
                    lastSeenPos = myPlayer.transform.position;
                    transform.position = Vector2.MoveTowards(transform.position, myPlayer.transform.position, speed * Time.deltaTime);
                    
                }
                else
                {
                transform.position = Vector2.MoveTowards(transform.position, lastSeenPos, speed * Time.deltaTime);
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, lastSeenPos, speed * Time.deltaTime);
            } 

        }


    }
}

