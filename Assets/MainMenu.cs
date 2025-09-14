using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuObject;
    public GameObject OptionsMenu;
    public SpriteRenderer[] SpriteRenderers;
    public GameObject BrightnessSlider;
    public GameObject GameplayOverall;
    public GameObject MainMenuOverall;
    public GameObject Player;
    public GameObject Difficulty;
    public GameObject ZoomSlider;
    public GameObject PlayerInfo;
    void Start()
    {
        MainMenuObject = GameObject.Find("MainMenu");
        OptionsMenu = GameObject.Find("OptionsMenu");
        SpriteRenderers = FindObjectsOfType<SpriteRenderer>();

    }
    // Start is called before the first frame update
    public void Play()
    {
        GameplayOverall.SetActive(true);
        MainMenuOverall.SetActive(false);
        
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Save()
    {
        
        
        
        PlayerPrefs.SetFloat("Health", Player.GetComponent<PlayerHealth>().health);
        PlayerPrefs.SetFloat("Speed", Player.GetComponent<PlayerMovement>().moveSpeed);
        PlayerPrefs.SetInt("Damage", Player.GetComponent<PlayerInfo>().playerDamage);
        PlayerPrefs.SetFloat("ProjectileShootCooldown", Player.GetComponent<PlayerShoot>().playerShootCooldown);
        PlayerPrefs.SetFloat("ProjectileSpeed", Player.GetComponent<PlayerShoot>().speed);
        PlayerPrefs.SetFloat("ProjectileDamage", Player.GetComponent<PlayerShoot>().ProjectileDamage);
        PlayerPrefs.SetFloat("ProjectileSizeX", Player.GetComponent<PlayerShoot>().ProjectileSize.x);
        PlayerPrefs.SetFloat("ProjectileSizeY", Player.GetComponent<PlayerShoot>().ProjectileSize.y);
        PlayerPrefs.SetFloat("ProjectileSizeZ", Player.GetComponent<PlayerShoot>().ProjectileSize.z);
        PlayerPrefs.SetInt("Level", Difficulty.GetComponent<LevelStats>().Level);
        PlayerPrefs.SetFloat("Difficulty", Difficulty.GetComponent<LevelStats>().Difficulty);
        
        PlayerPrefs.Save();
        
    
    }
    public void Load()
    {
        ZoomSlider.GetComponent<UnityEngine.UI.Slider>().value = PlayerPrefs.GetFloat("Zoom");
        Player.GetComponent<PlayerInfo>().health = PlayerPrefs.GetFloat("Health");
        Player.GetComponent<PlayerMovement>().moveSpeed = PlayerPrefs.GetFloat("Speed");
        Player.GetComponent<PlayerInfo>().playerDamage = PlayerPrefs.GetInt("Damage");
        Player.GetComponent<PlayerShoot>().playerShootCooldown = PlayerPrefs.GetFloat("ProjectileShootCooldown");
        Player.GetComponent<PlayerShoot>().speed = PlayerPrefs.GetFloat("ProjectileSpeed");
        Player.GetComponent<PlayerShoot>().ProjectileDamage = PlayerPrefs.GetFloat("ProjectileDamage");
        Player.GetComponent<PlayerShoot>().ProjectileSize.x = PlayerPrefs.GetFloat("ProjectileSizeX");
        Player.GetComponent<PlayerShoot>().ProjectileSize.y = PlayerPrefs.GetFloat("ProjectileSizeY");
        Player.GetComponent<PlayerShoot>().ProjectileSize.z = PlayerPrefs.GetFloat("ProjectileSizeZ");
        Difficulty.GetComponent<LevelStats>().Level = PlayerPrefs.GetInt("Level");
        Difficulty.GetComponent<LevelStats>().Difficulty = PlayerPrefs.GetFloat("Difficulty");
        Debug.Log("Loaded");
        Debug.Log(PlayerPrefs.GetFloat("Health"));
        PlayerPrefs.Save();
    }

}
