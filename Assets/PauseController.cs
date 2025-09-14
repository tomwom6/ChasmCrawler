using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject GameplayOverall;
    public GameObject MainMenuOverall;

    void Update()
    {
        if (Input.GetKeyDown("p") && GameObject.Find("Staircase").GetComponent<StaircaseController>().LevelChanging == false)
        {
            Debug.Log("paused");
            GameplayOverall.SetActive(false);
            MainMenuOverall.SetActive(true);
        }
        
    }
}
