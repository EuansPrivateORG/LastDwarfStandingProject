using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmUpgradeManagerUI : MonoBehaviour
{




    public GameObject farm_UI;
    public AudioSource Farm_UIaudio;
    public CurrencyManager currencyManager;
    public PassiveIncomeFarmManager passiveIncomeFarmManager;

    public GameObject BuyFarmButton;
    public GameObject UpgradeFarmButton;



    //public Text CostBaseHealthUpgradeText;





    private void Awake()
    {
        passiveIncomeFarmManager.UpdateFarmUpgradeCost();
        passiveIncomeFarmManager.UpdateBuyFarmCost();
    }
    void Start()
    {


        farm_UI.SetActive(false);
        UpgradeFarmButton.SetActive(false);

        passiveIncomeFarmManager = GetComponentInParent<PassiveIncomeFarmManager>();
        currencyManager = FindObjectOfType<CurrencyManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (farm_UI.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                BuyFarm();
                passiveIncomeFarmManager.UpdateBuyFarmCost();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1) && passiveIncomeFarmManager.IsFarmBought)
            {
                UpgradeFarm();
                passiveIncomeFarmManager.UpdateFarmUpgradeCost();
            }
        }


    }

    /* public void FarmMenuToggle()
     {

         if (farm_UI.activeInHierarchy)
         {
             ResumeLevelFarm();
         }
         else
         {
             PauseLeveFarm();
         }
         if (Farm_UIaudio != null) Farm_UIaudio.Play();
         else Debug.LogError("respawn_UIaudio not found");
     }*/

    public void ResumeLevelFarm()
    {
        farm_UI.SetActive(false);
    }

    public void PauseLeveFarm()
    {
        farm_UI.SetActive(true);
    }


    public void BuyFarm()
    {
        //Debug.Log("Buying Farm");
        passiveIncomeFarmManager.BuyPassiveIncomeFarm();


    }

   /* public void BuyFarm2()
    {
        //Debug.Log("Buying Farm");
        passiveIncomeFarmManager.BuyPassiveIncomeFarm2();


    }

    public void BuyFarm3()
    {
        //Debug.Log("Buying Farm");
        passiveIncomeFarmManager.BuyPassiveIncomeFarm3();


    }*/

    public void UpgradeFarm()
    {
        passiveIncomeFarmManager.UpgradePassiveIncomeFarm();
        //Debug.Log("Upgrading Farm");
    }


}
