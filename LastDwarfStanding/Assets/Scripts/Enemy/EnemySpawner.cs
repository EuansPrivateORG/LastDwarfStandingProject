using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject meleeSpawnLocation;
    public GameObject meleeEnemyPrefab;

    public GameObject rangedSpawnLocation;
    public GameObject rangedEnemyPrefab;

    public GameObject siegeSpawnLocation;
    public GameObject siegeEnemyPrefab;

    //public SpriteRenderer[] spriteRenderersInScene;
    public DayNightManager dayNightManager;

    private void Start()
    {
        //dayNightManager = GetComponent<DayNightManager>();
    }
     

    public GameObject SpawnEmemyMelee()
    {
        
        GameObject newEnemy = Instantiate(meleeEnemyPrefab, meleeSpawnLocation.transform.position, meleeSpawnLocation.transform.rotation);
        //newEnemy.GetComponent<SpriteRenderer>();
        newEnemy.transform.parent = transform;
        //Debug.Log("Spawning Enemy");
        dayNightManager.SpritesInScene.Add(newEnemy.GetComponent<SpriteRenderer>());

        //Debug.Log("Adding Enemy to list" + newEnemy);

        return newEnemy;
    }

    public GameObject SpawnEmemyRanged()
    {
        GameObject newEnemy = Instantiate(rangedEnemyPrefab, rangedSpawnLocation.transform.position, rangedSpawnLocation.transform.rotation);
        //newEnemy.GetComponent<SpriteRenderer>();
        newEnemy.transform.parent = transform;
        //Debug.Log("Spawning Enemy");
        dayNightManager.SpritesInScene.Add(newEnemy.GetComponent<SpriteRenderer>());
       // Debug.Log("Adding Enemy to list" + newEnemy);
        return newEnemy;
    }

    public GameObject SpawnEmemySiege()
    {
        GameObject newEnemy = Instantiate(siegeEnemyPrefab, siegeSpawnLocation.transform.position, siegeSpawnLocation.transform.rotation);
        //newEnemy.GetComponent<SpriteRenderer>();
        newEnemy.transform.parent = transform;
        dayNightManager.SpritesInScene.Add(newEnemy.GetComponent<SpriteRenderer>());
        //Debug.Log("Adding Enemy to list" + newEnemy);
        //Debug.Log("Spawning Enemy");

        return newEnemy;
    }

}
