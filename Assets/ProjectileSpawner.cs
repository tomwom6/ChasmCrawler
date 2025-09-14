using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public bool RangedMonsterCanAttack = true;
    public GameObject myPlayer;
    public float AttackCooldownUpper = 1.5f;

    public Monsters MonstersScript;
    public GameObject MonstersObject;
    public bool CanSeePlayer;
    private LayerMask myLayerMask;
    private LayerMask myLayerMask1;
    private LayerMask myLayerMask2;
    void OnEnable()
    {
        RangedMonsterCanAttack = true;
    }
    void Start()
    {
        myPlayer = GameObject.Find("Player");
        MonstersObject = GameObject.Find("MonstersObject");
        MonstersScript = MonstersObject.GetComponent<Monsters>();
        myLayerMask1 = LayerMask.GetMask("ExcludeRaycast");
        myLayerMask2 = LayerMask.GetMask("Monsters");
        myLayerMask = myLayerMask1 | myLayerMask2;
    }

    // Update is called once per frame
    void Update()
    {
        float dist1 = Vector2.Distance(myPlayer.transform.position, transform.position);  
        if (dist1 < 10.0 && RangedMonsterCanAttack && CanSeePlayer)
            {
                if (gameObject.tag == ("RangedMonster"))
                {
                    Instantiate(MonstersScript.Projectile, transform.position, transform.rotation);
                
                }  
                if (gameObject.tag == ("HomingRangedMonster"))
                {
                    Instantiate(MonstersScript.ProjectileHoming, transform.position, transform.rotation);
                }
                RangedMonsterCanAttack=false;
                StartCoroutine(RangedMonsterAttackCooldown(Random.Range(1,AttackCooldownUpper)));
            }
        if (myPlayer != null)
        {

            Vector2 direction = myPlayer.transform.position - transform.position;
            RaycastHit2D[] hits = (Physics2D.RaycastAll(transform.position, direction, 8f, ~myLayerMask));

            if (hits.Length>0)
            {
                if (hits[0].collider.gameObject.name == ("Player"))
                
                {
                    CanSeePlayer = true;
                    
                }
                else
                {
                    CanSeePlayer = false;
                }
            }
        }
    }
    IEnumerator RangedMonsterAttackCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        RangedMonsterCanAttack = true;
    }
}       
