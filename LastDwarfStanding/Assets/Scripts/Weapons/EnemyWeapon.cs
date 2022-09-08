using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float damage;
    public bool didDamage;
    public EnemySoundManager enemySoundManager;
    public Animator animator;
    public Animator animatorEnemySprite;

    
    private void Start()
    {
        didDamage = true;
        enemySoundManager = GetComponentInParent<EnemySoundManager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

    }
    public void OnCollisionStay(Collision other)
    {

        if (other.gameObject.tag != "EnemyMelee" && other.gameObject.tag != "EnemyWeapon")
        {
            if (other.gameObject.tag == "Player" && !didDamage)
            {
                
                animatorEnemySprite.SetTrigger("SkeletonAttack");
                
                didDamage = true;
               // Debug.Log("Damageing Player");
                other.gameObject.GetComponent<PlayerHealthManager>().TakeDamage(damage);
                other.gameObject.GetComponent<PlayerNavigationManager>().animator.SetTrigger("Hit");
                //Debug.Log("hitting"+ name);
                
                enemySoundManager.PlayAudioClip("TakeDamage");

            }
            if (other.gameObject.tag == "Base" && !didDamage)
            {
                if (gameObject.tag == "EnemyMelee")
                {
                    animatorEnemySprite.SetTrigger("SkeletonAttack");
                }
                didDamage = true;
                other.gameObject.GetComponent<BaseHealthManager>().TakeDamage(damage);
                enemySoundManager.PlayAudioClip("TakeDamage");
            }
        }

        if (other.gameObject.tag != "EnemySiege" && other.gameObject.tag != "EnemySiegeWeapon" && other.gameObject.tag != "EnemyMelee" && other.gameObject.tag != "EnemyWeapon")
        {

            //Debug.Log("EnemySiege IS Damageing" + name);
            if (other.gameObject.tag == "Player" && !didDamage)
            {
                Debug.Log("Siege Attacking Player"); 
                animatorEnemySprite.SetTrigger("SiegeAttack");
               
                    didDamage = true;
                other.gameObject.GetComponent<PlayerHealthManager>().TakeDamage(damage);
                enemySoundManager.PlayAudioClip("HammerCrush");
                //Debug.Log("Played Sound" + enemySoundManager.name);

            }
            if (other.gameObject.tag == "Base" && !didDamage)
            {
                Debug.Log("Siege Attacking Base");

                animatorEnemySprite.SetTrigger("SiegeAttack");
                
                didDamage = true;
                other.gameObject.GetComponent<BaseHealthManager>().TakeDamage(damage);
                enemySoundManager.PlayAudioClip("HammerCrush");
            }

        }



        if (other.gameObject.tag != "EnemyRanger" && other.gameObject.tag != "Arrow")
        {
            if (other.gameObject.tag == "Player" && !didDamage)
            {
                didDamage = true;
                //Debug.Log("Damageing Player");
                other.gameObject.GetComponent<PlayerHealthManager>().TakeDamage(damage);
                enemySoundManager.PlayAudioClip("ArrowHitFlesh");

            }

            if (other.gameObject.tag == "Base" && !didDamage)
            {
                didDamage = true;
                other.gameObject.GetComponent<BaseHealthManager>().TakeDamage(damage);
                enemySoundManager.PlayAudioClip("ArrowHitShield");
            }






        }
    }
}






    





