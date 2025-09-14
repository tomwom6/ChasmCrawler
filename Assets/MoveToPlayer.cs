using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    public float distFromPlayer;
    public GameObject gameObjectForMove;
    public GameObject myPlayer;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GameObject.FindWithTag("Player");
        
        myPlayer = GameObject.Find("Player");
        
    }
    
    // Update is called once per frame

    void Update()
    {
        
        if (myPlayer != null){
            distFromPlayer = Vector2.Distance(gameObjectForMove.transform.position, myPlayer.transform.position);
            if (distFromPlayer < 8)
            {
            transform.position = Vector2.MoveTowards(gameObjectForMove.transform.position, myPlayer.transform.position, speed * Time.deltaTime);
        }
        }
        

    }
}
