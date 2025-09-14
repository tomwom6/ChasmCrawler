using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    public void checkPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length == 0)
        {
            Debug.LogError("No player objects found");
        }
        else
        {
            Debug.Log("Found " + players.Length + " player objects");

            foreach (GameObject player in players)
            {
                Debug.Log("Player name: " + player.name);
            }
        }
    }
}
