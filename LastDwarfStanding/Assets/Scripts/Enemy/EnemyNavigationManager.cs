using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum eEnemyState
{
    MoveTowardsBase,
    MoveTowardsTarget,
    AttackTarget,
    StepBack
}




public class EnemyNavigationManager : MonoBehaviour


{
    public bool isEnemyActive = true;

    public float attackRange;
    
    public float enemySwingDelay;
    public float _swingTimer;

    public GameObject weapon;

    public eEnemyState enemyState;

    public GameObject _target;
    private GameObject _player;
    private GameObject _base;
    public GameObject _stepBack;

    private NavMeshAgent _agent;

    private PauseGame _pauseGame;

    public float stoppingRange;

    public float stepBackDistance;

    private EnemyHealthManager _enemyHealthManager;
    private EnemyRangedWeaponManager _enemyRangedWeaponManager;
    private EnemyWeapon _enemyWeapon;
    private Animator _animator;

    public int laneNumber;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyHealthManager = GetComponent<EnemyHealthManager>();
        _enemyRangedWeaponManager = GetComponent<EnemyRangedWeaponManager>();
        _enemyWeapon = GetComponent<EnemyWeapon>();
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _base = GameObject.FindGameObjectWithTag("Base");

        _stepBack = transform.GetChild(2).gameObject;
        _pauseGame = FindObjectOfType<PauseGame>();

       
    }

    // Start is called before the first frame update
    void Start()
    {
        SetEnemyState(eEnemyState.MoveTowardsBase);
    }


    // Update is called once per frame
    void Update()
    {
       

        if (!isEnemyActive ||!_enemyHealthManager.IsAlive) return;
   
        MovementLogic();

        EnemyNavLogic();

        EnemyOnPause();
    }

    private void MovementLogic()
    {
        if (enemyState == eEnemyState.AttackTarget) return;

        if (PathisClear())
        {
            //print(name + " path is clear");
            _agent.isStopped = false;
            if(gameObject.tag == "EnemyMelee")
            {
            _animator.SetTrigger("SkeletonWalk");

            }
            if(gameObject.tag == "EnemyRanger")
            {
                _animator.SetTrigger("RangedFly");
            }
            if (gameObject.tag == "EnemySiege")
            {
                _animator.SetTrigger("SiegeWalk");
            }
            _agent.destination = _target.transform.position;
        }
        
        else
        {
            //print(name + " path is not clear");
            if (gameObject.tag == "EnemyMelee")
            {
                _animator.SetTrigger("SkeletonIdle");
                _agent.destination = transform.position;
                _agent.isStopped = true;
            }
            if (gameObject.tag == "EnemySiege")
            {
                _animator.SetTrigger("SiegeIdle");
                _agent.destination = transform.position;
                _agent.isStopped = true;
            }
            //_agent.destination = transform.position;
            //_agent.isStopped = true;
        }
    }

    private void EnemyNavLogic()
    {
        if (!PathisClear()) return;

        float distanceToPlayer = transform.position.x - _player.transform.position.x;
        float distanceToBase = transform.position.x - _base.transform.position.x ;
        float distanceToTarget =transform.position.x - _target.transform.position.x;
        float distanceToStepBack = transform.position.x - _stepBack.transform.position.x;

        if(distanceToPlayer < 0 ) distanceToPlayer *= -1;
        if(distanceToTarget < 0) distanceToTarget *= -1;
        if(distanceToBase < 0) distanceToBase *= -1;
        if(distanceToStepBack < 0) distanceToStepBack *= -1;



        if (distanceToTarget < attackRange && enemyState != eEnemyState.StepBack)
        {
            if (distanceToTarget < attackRange - stepBackDistance)
            {
                if (!isEnemyActive) return;
                SetEnemyState(eEnemyState.StepBack);
            }
            else
            {
                if (!isEnemyActive) return;
                
                SetEnemyState(eEnemyState.AttackTarget);
            }
        }
        else if (distanceToPlayer > attackRange)
        {
            if (distanceToPlayer < distanceToBase)
            {
                if (!isEnemyActive) return;
                SetEnemyState(eEnemyState.MoveTowardsTarget);
            }
            else if (distanceToPlayer > distanceToBase)
            {
                if (!isEnemyActive) return;
                SetEnemyState(eEnemyState.MoveTowardsBase);
            }
        }

    }

    private bool PathisClear()
    {
        Vector3 direction = new Vector3(-1, 0 ,0);
        Debug.DrawRay(this.transform.position, direction * stoppingRange, Color.yellow);
        RaycastHit[] hits = Physics.RaycastAll(this.transform.position, direction, stoppingRange);
        if (hits.Length>0)
        {
            foreach (RaycastHit hit in hits)
            {
                EnemyNavigationManager enemy = hit.transform.gameObject.GetComponent<EnemyNavigationManager>();

                if (enemy != null && enemy != this && laneNumber == enemy.laneNumber)
                {

                    return false;
                }
            }
        }
        else
        {
            //print(name + "not getting any hits");
        }

        return true;
    }

    private void Attack()
    {
 
            _swingTimer += Time.deltaTime;
            if (_swingTimer >= enemySwingDelay)
            {
                if(gameObject.tag == "EnemyMelee")
            {
                weapon.GetComponent<Animator>().SetTrigger("Swing");
                GetComponentInChildren<EnemyWeapon>().didDamage = false;
                //_animator.SetTrigger("SkeletonAttack");
               // Debug.Log(gameObject.name + "Tried to do damage");

            }
                if(gameObject.tag == "EnemySiege")
            {

                weapon.GetComponent<Animator>().SetTrigger("Club");
                //Debug.Log(gameObject.name + "Tried to do damage");
                GetComponentInChildren<EnemyWeapon>().didDamage = false;
                _animator.SetTrigger("SiegeAttack");
            }
                //Debug.Log("Siege attacking animation");
                _swingTimer = 0;
                
            }
             else
             {
                  return;
             }

    }






    private void SetEnemyState(eEnemyState newState)
    {
        enemyState = newState;

        switch (enemyState)
        {
            case eEnemyState.MoveTowardsBase:
                _agent.isStopped = false;
                _target = _base;
                break;

            case eEnemyState.MoveTowardsTarget:
                _agent.isStopped = false;
                _target = _player;
                break;

            case eEnemyState.AttackTarget:
                _agent.isStopped = true;
                if (gameObject.tag == "EnemyMelee")
                {
                    _animator.SetTrigger("SkeletonIdle");
                }
                if (gameObject.tag == "EnemySiege")
                {
                    _animator.SetTrigger("SiegeIdle");
                }
                if (gameObject.tag != ("EnemyRanger"))
                {

                Attack();
                }
                break;

            case eEnemyState.StepBack:
                
                float stepBackDistance = Vector3.Distance(transform.position, _stepBack.transform.position);
                _stepBack.transform.position = transform.position + new Vector3(1.5f, 0, 0);
                
                if (stepBackDistance > 0.1f)
                {
                    _agent.isStopped = false;
                    _target = _stepBack;
                    _agent.destination = _target.transform.position;
                }
                else
                {
                    //enemyState = eEnemyState.AttackTarget;
                    _target = _player;
                }
                break;
        }

        if (enemyState != eEnemyState.AttackTarget)
        {
            _agent.destination = _target.transform.position;
        }
    }

    public void EnemyOnPause()
    {
        if (!isEnemyActive)
        {
            //Debug.Log("Enemy Stopped");
            if (gameObject.tag == "EnemyMelee")
            {
                _animator.SetTrigger("SkeletonIdle");
            }
            if (gameObject.tag == "EnemyRanged")
            {
                _animator.SetTrigger("RangedFly");
            }
            if (gameObject.tag == "EnemySiege")
            {
                _animator.SetTrigger("SiegeIdle");
            }
            _animator.SetTrigger("SkeletonIdle");
            _agent.destination = transform.position;



        }
    }
    public NavMeshAgent navMeshAgent { get { return _agent; } }
}
