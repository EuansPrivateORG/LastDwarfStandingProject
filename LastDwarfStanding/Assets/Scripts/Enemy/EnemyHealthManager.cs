using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthManager : MonoBehaviour
{
    public float enemyHealth;
    public float enemyMaxHealth;
    public Image enemyHealthBar;
    private Transform _parent;

    public bool LootDropped = false;
    private bool isAlive = true;
    public GameObject loot;
    public Transform spawnPosition;
    private EnemyNavigationManager navigationManager;

    public EnemySoundManager enemySoundManager;
    private Animator _animator;

    //public DayNightManager dayNightManager;

    // Start is called before the first frame update
    void Start()
    {
        //dayNightManager = GetComponent<DayNightManager>();
        _animator = GetComponent<Animator>();
        navigationManager = GetComponent<EnemyNavigationManager>();
        enemySoundManager = GetComponent<EnemySoundManager>();
        enemyHealth = enemyMaxHealth;
        _parent = enemyHealthBar.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        _parent = enemyHealthBar.transform.parent;
        if (LootDropped)
        {
            SpawnLoot();
        }
    }
    public void TakeDamage(float damageToTake)
    {
        if (!isAlive) return;

        if (enemyHealth - damageToTake <= 0)
        {
            enemyHealth = 0;

            OnDeath();
        }
        else
        {
            enemyHealth -= damageToTake;
            if (gameObject.tag == "EnemyMelee")
            {
                _animator.SetTrigger("SkeletonHit");

            }
            if (gameObject.tag == "EnemyRanged")
            {
                _animator.SetTrigger("RangedHit");
            }
            if (gameObject.tag == "EnemySiege")
            {
                _animator.SetTrigger("SiegeHit");
            }
        }
        enemyHealthBar.fillAmount = enemyHealth / enemyMaxHealth;

        //print(name + " taking damage.");

    }


    private void OnDeath()
    {
        if (!isAlive) return;
        isAlive = false;
        navigationManager.navMeshAgent.isStopped = true;
        if (gameObject.tag == "EnemyMelee")
        {
            _animator.SetTrigger("SkeletonDeath");
        }
        if (gameObject.tag == "EnemySiege")
        {
            _animator.SetTrigger("SiegeDeath");
        }
        if (gameObject.tag == "EnemyRanged")
        {
            _animator.SetTrigger("RangedDeath");
        }
        FindObjectOfType<EnemyWaveManager>().EnemnyDied(this.gameObject);
        gameObject.tag = "DeadEnemy";
        if (!LootDropped)
        {
            SpawnLoot();
            enemySoundManager.PlayAudioClip("EnemyDeath");
        }



        Destroy(gameObject, 0.7f);
        LootDropped = false;
    }

    private void SpawnLoot()
    {
        //Debug.Log("Dropping Coin");
        GameObject LootClone = Instantiate(loot, spawnPosition.position, spawnPosition.rotation);
        LootDropped = true;

    }

    public bool IsAlive { get { return isAlive; } }
}
