using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{

    public GameObject arrow;
    public Transform arrowSpawnPosition;
    public float timeSinceLastArrow;
    public float reloadTime;
    public Transform pivotPoint;
    public List<EnemyHealthManager> enemyInSceneList;

    public Transform nearestEnemy;



    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastArrow = 0f;
    }

    // Update is called once per frame
    void Update()
    {


        //var gameobj = FindObjectsOfType<EnemyHealthManager>();
        enemyInSceneList.AddRange(FindObjectsOfType<EnemyHealthManager>());

        float closestEnemyPos = 10000;
        if (enemyInSceneList.Count > 0)
        {
            timeSinceLastArrow += Time.deltaTime;

            if (timeSinceLastArrow >= reloadTime)
            {
                RangedAttack();

            }

            foreach (EnemyHealthManager enemy in enemyInSceneList)
            {
                if (enemy.transform.position.x + transform.position.x < closestEnemyPos)
                {
                    nearestEnemy = enemy.transform;
                }
            }

            //pivotPoint.LookAt(new Vector3(nearestEnemy.position.x, nearestEnemy.position.y, pivotPoint.transform.position.z));
            pivotPoint.LookAt(nearestEnemy.position);
            enemyInSceneList.Clear();
        }
    }

    public void RangedAttack()
    {

        GameObject projectileClone = Instantiate(arrow, arrowSpawnPosition.position, arrowSpawnPosition.rotation);
        timeSinceLastArrow = 0;


    }



}
