using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for handling player shooting actions
public class PlayerShoot : MonoBehaviour
{
    // Variables for tracking mouse input, player, and projectile settings
    public bool mouseClicked = false;  // Flag to track mouse click
    public Vector3 mousePos;  // Stores mouse position in world coordinates
    public GameObject myPlayer;  // Reference to the Player GameObject
    public Monsters MonstersScript;  // Reference to the Monsters script
    public GameObject Monsters;  // GameObject containing the Monsters script
    public bool canShoot = true;  // Flag to control shooting ability
    public float playerShootCooldown = 0.5f;  // Time between shots
    public Vector3 ProjectileSize = new Vector3(0.5f, 0.5f, 0.5f);  // Size of player projectiles
    public float speed = 3.0f;  // Movement speed of player projectiles
    public float timeBeforeDie = 5.0f;  // Duration before player projectiles disappear
    public float ProjectileDamage = 2f;  // Damage dealt by player projectiles
    public bool isShooting = false;  // Flag to track if player is shooting


    // Set shooting ability to true when enabled
    void OnEnable()
    {
        canShoot = true;
    }

    // Find required GameObjects and components
    void Start()
    {
        myPlayer = GameObject.Find("Player");
        Monsters = GameObject.Find("MonstersObject");
        MonstersScript = Monsters.GetComponent<Monsters>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for left mouse click, cooldown, and shooting ability
        if (Input.GetMouseButtonDown(0) && mouseClicked == false && canShoot == true)
        {
            // Get mouse position in world coordinates
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseClicked = true;
        }

        // If mouse click is registered
        if (mouseClicked == true)
        {
            isShooting = true;  // Set shooting flag to true
            // Reset click flag and shooting ability
            mouseClicked = false;
            canShoot = false;
            
            // Instantiate player projectile
            GameObject PlayerProjectile = Instantiate(MonstersScript.PlayerProjectile, transform.position, transform.rotation);
            PlayerProjectile.transform.localScale = ProjectileSize;
            PlayerProjectile.GetComponent<ProjectilePlayer>().speed = speed;
            PlayerProjectile.GetComponent<ProjectilePlayer>().timeBeforeDie = timeBeforeDie;
            PlayerProjectile.GetComponent<ProjectilePlayer>().ProjectileDamage = ProjectileDamage;
            // Start cooldown coroutine
            StartCoroutine(ShootCooldown(playerShootCooldown));
            
        }
    }

    // Coroutine to handle shooting cooldown
    IEnumerator ShootCooldown(float playerShootCooldown)
    {
        yield return new WaitForSeconds(0.2f);
        isShooting = false;
        yield return new WaitForSeconds(playerShootCooldown-0.2f);
        canShoot = true;  // Enable shooting again after cooldown
    }
    
}
