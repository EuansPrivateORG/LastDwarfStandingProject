using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponManager : MonoBehaviour
{
    //public bool canAttack = true;

    public Transform weaponPosition;
    


    private GameObject _enemyMelee;
    private GameObject _enemyRanger;
    private GameObject _enemySiege;
    private EnemyNavigationManager _enemyNavigationManager;
    private PauseGame _pauseGame;



    private void Awake()
    {
        _enemyNavigationManager = FindObjectOfType<EnemyNavigationManager>();
  
        _pauseGame = FindObjectOfType<PauseGame>();

    }

    // Start is called before the first frame update
    void Start()
    {

        _enemyMelee = GameObject.FindGameObjectWithTag("EnemyMelee");
        _enemyRanger = GameObject.FindGameObjectWithTag("EnemyRanger");
        _enemySiege = GameObject.FindGameObjectWithTag("EnemySiege");
    }

    // Update is called once per frame
    void Update()
    {
  

    }

}


