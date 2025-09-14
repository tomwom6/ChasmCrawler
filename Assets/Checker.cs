using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker: MonoBehaviour
{
    private PlayerManager playerManager;

    void Start()
    {
        playerManager = new PlayerManager();
        playerManager.checkPlayers();
    }
}

