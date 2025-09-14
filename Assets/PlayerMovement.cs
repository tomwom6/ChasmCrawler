using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{   
    private bool DoingDodge = false;
    public float moveSpeed = 5f;

    private float horizontal;
    private  float vertical;
    private Rigidbody2D rb;     
    public float startDodgeTime = 0.5f;
    private float timeBetweenDodge;
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public Sprite leftSprite; // Sprite for left movement
    public Sprite idleSprite;
    public Sprite rightSprite; // Sprite for right movement
    public Sprite leftShootSprite; // Sprite for left movement
    public Sprite rightShootSprite; // Sprite for right movement
    public Sprite leftAttackSprite;
    public Sprite rightAttackSprite;
    public PlayerShoot PlayerShoot;
    public Animator animator;

    
    void Start()
        {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        PlayerShoot = GetComponent<PlayerShoot>();
        animator = GetComponent<Animator>();
        

        }
    void Update()
        {

        // Check if idle animation should be played (prev animation finished, not shooting, standing still)
        
        if (PlayerShoot.isShooting == false){
    
            if (rb.velocity.x < 0)
            {
                spriteRenderer.sprite = leftSprite;
                
            }
            else if (rb.velocity.x > 0)
            {
                spriteRenderer.sprite = rightSprite;
            
            }
            
        }   
        if (PlayerShoot.isShooting == true){
            if (rb.velocity.x < 0)
            {
                spriteRenderer.sprite = leftShootSprite;
                StartCoroutine(Revert());
            }
            else if (rb.velocity.x > 0)
            {
                spriteRenderer.sprite = rightShootSprite;
                StartCoroutine(Revert());
            }
            else if (rb.velocity.x == 0)
            {
                if (spriteRenderer.sprite == leftSprite)
                {
                    spriteRenderer.sprite = leftShootSprite;
                    StartCoroutine(Revert());
                }
                else if (spriteRenderer.sprite == rightSprite)
                {
                    spriteRenderer.sprite = leftShootSprite;
                    StartCoroutine(Revert());
                }
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (rb.velocity.x < 0)
            {
                spriteRenderer.sprite = leftAttackSprite;
                StartCoroutine(Revert());
               
            }
            else if (rb.velocity.x > 0)
            {
                spriteRenderer.sprite = rightAttackSprite;
                StartCoroutine(Revert());
            }
            else if (rb.velocity.x == 0)
            {
                if (spriteRenderer.sprite == leftSprite)
                {
                    spriteRenderer.sprite = leftAttackSprite;
                    StartCoroutine(Revert());
                }
                else if (spriteRenderer.sprite == rightSprite)
                {
                    spriteRenderer.sprite = rightAttackSprite;
                    StartCoroutine(Revert());
                }
            }
        }

        if (Input.GetKey(KeyCode.Q) && !DoingDodge && timeBetweenDodge < 0.0f)
            {       
                DoingDodge=true;
                rb.velocity = new Vector2(horizontal*20, vertical*20);
                StartCoroutine(DodgeDuration()); 
                timeBetweenDodge = startDodgeTime;
            }

        timeBetweenDodge-= Time.deltaTime;  
                
                
        if (DoingDodge == false)
        {
                horizontal = Input.GetAxis("Horizontal");
                vertical = Input.GetAxis("Vertical");
                rb.velocity = new Vector2(horizontal*moveSpeed, vertical*moveSpeed);    
        }
        }
    IEnumerator Revert()
    {
        
        yield return new WaitForSeconds(1f);
        if ((spriteRenderer.sprite == rightShootSprite) || (spriteRenderer.sprite == rightAttackSprite))
        {
            spriteRenderer.sprite = rightSprite;
        }
        if ((spriteRenderer.sprite == leftShootSprite) || (spriteRenderer.sprite == leftAttackSprite))
        {
            spriteRenderer.sprite = leftSprite;
        }
    }

    IEnumerator DodgeDuration()
    {
        yield return new WaitForSeconds(0.5f);
        DoingDodge=false;
        
    }

}
