using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public float damage;
    public bool didDamage;




    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Weapon" && !didDamage)
        {
            EnemyHealthManager enemyHealthManager = other.gameObject.GetComponent<EnemyHealthManager>();
            if (enemyHealthManager != null)
            {
                enemyHealthManager.TakeDamage(damage);
                didDamage = true;
            }



            //else Debug.LogError(name + " cannot find EnemyHealthManager.");

            //Debug.Log("Hit Something");
            /*if (other.gameObject.tag.Contains ("Enemy") && !didDamage)
            {
                //Debug.Log("Hit Enemy");
                didDamage = true;
                EnemyHealthManager enemyHealthManager = other.gameObject.GetComponent<EnemyHealthManager>();
                if (enemyHealthManager != null)
                {
                    enemyHealthManager.TakeDamage(damage);
                }
                else
                {
                    Debug.LogError(name + " cannot find EnemyHealthManager.");
                }
                

            }*/
        }
    }
}

