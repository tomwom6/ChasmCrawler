using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    // Variables
    public int openingDirection; // Controls which direction rooms open
    private int rand; // Used for random number generation
    public bool spawned = false; // Tracks whether the room has been spawned

    // References to other components
    private RoomTemplates templates; // Stores room templates
    public Rigidbody2D rigidbody2D; // For physics interactions
    public RoomTemplates Rooms; // Unclear purpose, might be redundant
    public GameObject myPlayer; // Reference to the player object
    public GameObject MonstersObject; // Unclear purpose
    public Monsters Monsters; // Component for managing monsters
    public string NameTemp; // Temporary string for naming rooms
    
      void Start()
    {
        // Finds references to necessary objects and components
        myPlayer = GameObject.Find("Player");
        templates = GameObject.Find("RoomTemplatesObject").GetComponent<RoomTemplates>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        MonstersObject = GameObject.Find("MonstersObject");
        Monsters = MonstersObject.GetComponent<Monsters>();

        // Schedules the Spawn function to run after a short delay
        Invoke("Spawn", 0.4f);
    }
    
    void Spawn(){
        if(spawned == false){
            if (openingDirection == 1) // depending on the spawnpoint door direction, it will spawn a random room that correlates to that door direction
            {
                rand = Random.Range(0, templates.bottomRooms.Length);
                if (rand == templates.bottomRooms.Length-1)
                    {
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    }
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                if (rand == templates.bottomRooms.Length-2)
                    {
                        Instantiate(Monsters.Monster1, transform.position, Quaternion.identity);
                        Instantiate(Monsters.RangedMonsterHoming, transform.position, Quaternion.identity);
                    }
                
            }
            else if(openingDirection == 2)
            {
                rand = Random.Range(0, templates.topRooms.Length);
                if (rand == templates.topRooms.Length-1)
                    {
                    rand = Random.Range(0, templates.topRooms.Length);
                    }
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                if (rand == templates.bottomRooms.Length-2)
                    {
                        Instantiate(Monsters.Monster1, transform.position, Quaternion.identity);
                        Instantiate(Monsters.RangedMonster, transform.position, Quaternion.identity);
                    }   
            }
            else if(openingDirection == 3)
            {
                
                rand = Random.Range(0, templates.leftRooms.Length);
                if (rand == templates.leftRooms.Length-1)
                    {
                        rand = Random.Range(0, templates.leftRooms.Length);
                    }
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                 if (rand == templates.bottomRooms.Length-2)
                    {
                        Instantiate(Monsters.Monster1, transform.position, Quaternion.identity);
                        Instantiate(Monsters.RangedMonster, transform.position, Quaternion.identity);
                    }
            }
            else if(openingDirection == 4)
            {
                rand = Random.Range(0, templates.rightRooms.Length);
                if (rand == templates.rightRooms.Length-1)
                    {
                        rand = Random.Range(0, templates.rightRooms.Length);
                    }
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                if (rand == templates.bottomRooms.Length-2)
                    {
                        Instantiate(Monsters.Monster1, transform.position, Quaternion.identity);
                        Instantiate(Monsters.RangedMonster, transform.position, Quaternion.identity);
                    }
            }
            // Updates spawn ID, room name, and adds it to the list of rooms
            templates.SpawnID = templates.SpawnID + 1;
            NameTemp = (gameObject.name + templates.SpawnID.ToString());
            gameObject.name = NameTemp;
            templates.Rooms.Add(gameObject);

            // Marks the room as spawned
            spawned = true;
            


        }
    }
    void OnTriggerEnter2D(Collider2D other){ // if the spawnpoint collides with another spawnpoint, it will spawn a closed room
        if(other.CompareTag("SpawnPoint")){
            if(other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                myPlayer = GameObject.Find("Player");
                float dist = Vector2.Distance(transform.position, myPlayer.transform.position);
                if (dist> 6f)
                {
                    Instantiate(templates.closedRoom, transform.position, transform.rotation);
                    Destroy(gameObject);
                    templates.SpawnID = templates.SpawnID + 1;
                }

            }
            spawned = true;
            
        }
    }
}