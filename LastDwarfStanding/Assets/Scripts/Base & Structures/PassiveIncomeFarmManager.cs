using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveIncomeFarmManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject farmColider;
    public float farmIncomeTimer;
    public float IncomeInterval = 10f;

    //public int costOfFarm;
    //public int currentCostOfFarm;

    public int currentIncome;

    public GameObject UnbuiltFarm;
    public GameObject BuiltFarm;
    //public GameObject BuiltFarm2;
    //public GameObject BuiltFarm3;
    [SerializeField] private FarmUpgradeManagerUI farmUpgradeManagerUI;

    public Text costOfFarmText;
    public Text CostOfFarmUpgradeText;

    public int currencyCostFarm = 1;
    public int CurrentCurrencyCostFarm = 1;

    public int currencyCostFarmUpgrade = 1;
    public int CurrentCurrencyCostFarmUpgrade = 1;

    public bool IsFarmBought;
    //public bool IsFarm2Bought;
    //public bool IsFarm3Bought;

    public int farmNumber;
    public List<GameObject> farms = new List<GameObject>();

    public CurrencyManager currencyManager;

    private void Awake()
    {
        //UpdateFarmUpgradeCost();
        //UpdateBuyFarmCost();
    }
    void Start()
    {

        //  FarmNumberFinder();
        //UpdateFarmUpgradeCost();
        //UpdateBuyFarmCost();

        IsFarmBought = false;
        //IsFarm2Bought = false;
        //IsFarm3Bought = false;

        farmColider.SetActive(true);
        BuiltFarm.SetActive(false);
        //BuiltFarm2.SetActive(false);
        //BuiltFarm3.SetActive(false);
        farmIncomeTimer += Time.deltaTime;
        currencyManager = FindObjectOfType<CurrencyManager>();
        if (farmUpgradeManagerUI == null)
        {
            farmUpgradeManagerUI = GetComponentInChildren<FarmUpgradeManagerUI>();

        }



    }

    // Update is called once per frame
    void Update()
    {

        foreach (GameObject farm in GameObject.FindGameObjectsWithTag("Farm"))
        {
            if (farms.Contains(farm))
            {
                if (IsFarmBought)
                {
                    farmIncomeTimer += Time.deltaTime;
                    PassiveIncomeOverTime();
                    Debug.Log("Farm 1 bought");

                }
               /* if (IsFarm2Bought && !IsFarmBought && IsFarm3Bought)
                {
                    farmIncomeTimer += Time.deltaTime;
                    PassiveIncomeOverTime();
                    Debug.Log("Farm 2 bought");

                }
                if (IsFarm3Bought && !IsFarmBought && IsFarm2Bought)
                {
                    farmIncomeTimer += Time.deltaTime;
                    PassiveIncomeOverTime();
                    Debug.Log("Farm 3 bought");

                }*/
                else
                {
                return;

                }
            }
            else
            {
                farms.Add(farm);

            }
            farmNumber += 1;
            //Debug.Log("farms" + farms);


        }
    }

        public void BuyPassiveIncomeFarm()
        {
            if (currencyManager.currentCurrencyAmount >= currencyCostFarm)
            {
                IsFarmBought = true;
                currencyManager.RemoveCurrency(currencyCostFarm);
                currencyManager.UpdateCurrencyText();

                // currencyManager.UpdateCurrencyText();

                //farmColider.SetActive(false);
                BuiltFarm.SetActive(true);
                Debug.Log("Setting to active" + BuiltFarm);
                currentIncome += 5;

                farmUpgradeManagerUI.BuyFarmButton.SetActive(false);
                farmUpgradeManagerUI.UpgradeFarmButton.SetActive(true);
            }
            else
            {
                return;
            }
        }
    /* public void BuyPassiveIncomeFarm2()
     {
         if (currencyManager.currentCurrencyAmount >= currencyCostFarm)
         {
             IsFarm2Bought = true;
             currencyManager.RemoveCurrency(currencyCostFarm);
             currencyManager.UpdateCurrencyText();

             // currencyManager.UpdateCurrencyText();

             //farmColider.SetActive(false);
             BuiltFarm2.SetActive(true);
             currentIncome += 5;

             farmUpgradeManagerUI.BuyFarmButton.SetActive(false);
             farmUpgradeManagerUI.UpgradeFarmButton.SetActive(true);
         }
         else
         {
             return;
         }
     }
     public void BuyPassiveIncomeFarm3()
     {
         if (currencyManager.currentCurrencyAmount >= currencyCostFarm)
         {
             IsFarm3Bought = true;
             currencyManager.RemoveCurrency(currencyCostFarm);
             currencyManager.UpdateCurrencyText();

             // currencyManager.UpdateCurrencyText();

             //farmColider.SetActive(false);
             BuiltFarm3.SetActive(true);
             currentIncome += 5;

             farmUpgradeManagerUI.BuyFarmButton.SetActive(false);
             farmUpgradeManagerUI.UpgradeFarmButton.SetActive(true);
         }
         else
         {
             return;
         }
     }*/

    public void UpgradePassiveIncomeFarm()
        {
            if (currencyManager.currentCurrencyAmount >= currencyCostFarmUpgrade)
            {
                currencyManager.RemoveCurrency(currencyCostFarmUpgrade);
                //currencyManager.UpdateCurrencyText();
                currencyCostFarmUpgrade += currencyCostFarmUpgrade + 6;
                UpdateFarmUpgradeCost();
                currentIncome += (currentIncome + 5 / 3);

            }

        }




    public FarmUpgradeManagerUI farmUpgradeUI
    {
        get { return farmUpgradeManagerUI; }

    }


    public void UpdateFarmUpgradeCost()
    {

        CostOfFarmUpgradeText.text = currencyCostFarmUpgrade.ToString();
    }

    public void UpdateBuyFarmCost()
    {

        costOfFarmText.text = currencyCostFarm.ToString();
    }

    public void PassiveIncomeOverTime()
    {
        if (farmIncomeTimer > IncomeInterval)
        {
            farmIncomeTimer = 0;
            currencyManager.currentCurrencyAmount += currentIncome;
            currencyManager.UpdateCurrencyText();

        }
    }


    public void FarmNumberFinder()
    {
        foreach (GameObject farm in GameObject.FindGameObjectsWithTag("Farm"))
        {
            if (farms.Contains(farm))
            {
                return;
            }
            else
            {
                farms.Add(farm);

            }
            farmNumber += 1;
            Debug.Log("farms" + farms);


        }
    }
}
