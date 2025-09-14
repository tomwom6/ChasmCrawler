using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelTextUpdater : MonoBehaviour
{
    public TMP_Text LevelText;
    public GameObject Difficulty;
    public LevelStats LevelStats;
    public string LevelTextPreset;
    void Start()
    {
        Difficulty = GameObject.Find("Difficulty");
        LevelStats = Difficulty.GetComponent<LevelStats>();
    }

    // Update is called once per frame
    void Update()
    {
        LevelTextPreset = ("Level " + LevelStats.Level.ToString()); 
        LevelText.text = LevelTextPreset;
    }
}
