using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using System;

public class EnemyWaveManager : MonoBehaviour
{

    private bool waveStarted = false;
    //public float waveTimer = 0f;
    //public float waveTimeLimit = 10f;
    //public float waveBreakTimer = 0f;
    //public float waveBreakTimeLimit = 5f;

    [Header("New Way")]
    private Animator waveAnimation;
    public DayNightManager dayNightManager;
    public List<GameObject> currentWave = new List<GameObject>();
    private EnemySpawner enemySpawner;
    private int waveCount = 1;
    public Text waveTextNumber;
    public float spawnRate = 0.5f;
    private float spawnTimer = 0;
    private int spawnedEnemies = 0;
    private int enemiesToSpawn = 0;
    private bool nextDayHasHappened = true;
    private bool waveTextSeen = false;

    private WaveConfig[] waveConfigs;

    // Start is called before the first frame update
    void Start()
    {
        waveAnimation = gameObject.GetComponent<Animator>();
        if (dayNightManager == null) dayNightManager = FindObjectOfType<DayNightManager>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        //waveBreakTimer += Time.deltaTime;
        //waveCount = 0;
        //WaveBreak();

        if (dayNightManager == null) Debug.LogError(name + " cannot find DayNightManager");
        waveConfigs = (WaveConfig[])Resources.LoadAll<WaveConfig>("");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!waveStarted)
        {
            WaveBreak();

        }
        else
        {
            Wave();
        }*/
        CreateWave(waveCount);

        if (!dayNightManager.isDayTime)
        {
            UpdateWaveText();
            SpawnWave();

        }

        CheckIfWaveFinished();
        CheckIfNextDayHappeded();
    }

    private void CheckIfNextDayHappeded()
    {
        if (nextDayHasHappened) return;

        if (dayNightManager.isDayTime)
        {
            nextDayHasHappened = true;
        }
    }

    /*    public void Wave()
        {
            waveStarted = true;
            waveTimer += Time.deltaTime;


            if (waveTimer >= waveTimeLimit)
            {
                waveStarted = false;

                waveBreakTimer = 0f;
                WaveBreak();

            }
    */




    private void CheckIfWaveFinished()
    {
        if (waveStarted && spawnedEnemies == enemiesToSpawn)
        {
            waveStarted = false;
            waveCount++;
        }
    }

    /*    public void WaveBreak()
        {
            waveBreakTimer += Time.deltaTime;

            if (waveBreakTimer >= waveBreakTimeLimit)
            {
                waveTimer = 0f;
                waveCount++;
                UpdateWaveText();
                waveAnimation.SetTrigger("TriggerWaveText");
                Wave();


            }
        }*/

    public void UpdateWaveText()
    {
        if (!waveTextSeen)
        {
            waveAnimation.SetTrigger("TriggerWaveText");
            waveTextNumber.text = waveCount.ToString();
            waveTextSeen = true;
        }

    }

    private void CreateWave(int waveNumber)
    {
        if (nextDayHasHappened)
        {
            enemiesToSpawn = NumberOfEnemies(waveNumber);
            spawnedEnemies = 0;
            spawnTimer = 0f;
            //print("New Wave, enemeis to spawn = " + enemiesToSpawn);
            waveTextSeen = false;
            waveStarted = true;
            nextDayHasHappened = false;
        }
    }

    private int NumberOfEnemies(int waveNumber)
    {
        return 1 + (waveNumber - 1);
    }

    private void SpawnWave()
    {
        //print("Spawning wave. WaveStarted=" + waveStarted + " SpawnedEnemies=" + spawnedEnemies + " of " + enemiesToSpawn);
        if (waveStarted && spawnedEnemies < enemiesToSpawn)
        {
            spawnTimer += Time.deltaTime;
            //Debug.Log("Spawn timer=" + spawnTimer);

            if (spawnTimer > spawnRate)
            {
                //if (waveCount >= 10 && spawnedEnemies < )
                currentWave.Add(enemySpawner.SpawnEmemyMelee());
                currentWave.Add(enemySpawner.SpawnEmemyRanged());
                currentWave.Add(enemySpawner.SpawnEmemySiege());
                spawnedEnemies++;
                spawnTimer = 0;
            }
        }
    }

    public void EnemnyDied(GameObject enemy)
    {
        currentWave.Remove(enemy);
        dayNightManager.SpritesInScene.Remove(enemy.GetComponent<SpriteRenderer>());
    }

    public int WaveCount { get { return waveCount; } }

}
