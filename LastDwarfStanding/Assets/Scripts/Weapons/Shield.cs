using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{


    public float maxShield;
    public float currentShield;
    public bool canBlock;
    

    private ShieldManager _shieldManager;

    private void Start()
    {
        canBlock = true;
        currentShield = maxShield;
        _shieldManager = FindObjectOfType<ShieldManager>();
        _shieldManager.UpdateShieldBar();
        gameObject.SetActive(false);
    }

    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with enemy");
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Shield")
        {
            if (other.gameObject.tag == "EnemyMelee")
            {

                if(currentShield <= 0)
                {
                    canBlock=false;
                    Debug.Log("Shield Broken");
                    
                }
            }
        }

    }

    public void ShieldDamage(float damage)
    {
       if (currentShield - damage <= 0)
        {
            canBlock = false;
            currentShield = 0;
            _shieldManager.UpdateShieldBar();
        }
        else
        {
            canBlock = true;
            currentShield -= damage;
            _shieldManager.UpdateShieldBar();
        }

    }
}
