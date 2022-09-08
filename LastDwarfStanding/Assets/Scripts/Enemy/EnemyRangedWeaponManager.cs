using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRangedWeaponManager : MonoBehaviour
//public bool canAttack = true;
{
    public Transform weaponPosition;



    private GameObject _enemy;
    private GameObject _player;
    private PlayerHealthManager _playerHealthManager;
    private EnemyNavigationManager _enemyNavigationManager;
    private PauseGame _pauseGame;

    public GameObject arrow;
    public Transform arrowSpawnPosition;
    public float timeSinceLastArrow;
    public float reloadTime;
    private bool _arrowFired;
    public NavMeshAgent _agent;
    public bool CanFire = true;
    private Animator _animator;




    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _enemyNavigationManager = FindObjectOfType<EnemyNavigationManager>();

        _pauseGame = FindObjectOfType<PauseGame>();

    }

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastArrow = 0f;

        _enemy = GameObject.FindGameObjectWithTag("EnemyRanger");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Arrow Timer" + timeSinceLastArrow);

        timeSinceLastArrow += Time.deltaTime;

        if (timeSinceLastArrow >= reloadTime && CanFire)
        {
            _arrowFired = false;

            RangedAttack();
            //ArrowOnPause();
        }


    }

    public void RangedAttack()
    {


        if (timeSinceLastArrow >= reloadTime && !_arrowFired && CanFire)
        {
            _arrowFired = true;
            _agent.isStopped = true;
            _animator.SetTrigger("RangedAttack");
            _animator.SetTrigger("RangedAttack2");
            GameObject projectileClone = Instantiate(arrow, arrowSpawnPosition.position, arrowSpawnPosition.rotation);
            timeSinceLastArrow = 0;


        }

    }
  /*  public void ArrowOnPause()
    {
        if (!CanFire)
        {
            //Debug.Log("Enemy Stopped");
            arrow.transform.position = transform.position;



        }
    
    }
  */
}



