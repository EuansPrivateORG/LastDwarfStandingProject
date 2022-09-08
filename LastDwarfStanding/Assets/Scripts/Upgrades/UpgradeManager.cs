using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerHealthManager playerHealthManager;
    public BaseHealthManager baseHealthManager;
    public PlayerWeapon playerWeapon;
    public CurrencyManager currencyManager;
    public ShieldManager shieldManager;
    public Shield shield;
    public ArrowFriendly arrowTurret;

    public GameObject upgrade_UI;
    public AudioSource upgrade_UIaudio;

    public GameObject turretBuildButton;
    public GameObject turretUpgradeButton;
    public GameObject turretBuildButtonSecond;
    public GameObject turretUpgradeButtonSecond;

    public GameObject turret;
    public GameObject turretSecond;

    public GameObject stoneWall;
    public GameObject woodenWall;
    public GameObject woodenPillar;
    public GameObject stonePillar;
    private bool isTurretBought;
    private bool isTurretSecondBought;

    public Text CostBaseHealthUpgradeText;
    public Text CostBaseRepairText;

    public Text CostTurretUpgradeText;
    public Text CostTurretInitialText;

    public Text CostTurretUpgradeSecondText;
    public Text CostTurretInitialSecondText;

    public int currencyCostBaseHealthUpgrade = 1;
    public int currenctCurrencyCostBaseHealthUpgrade = 1;

    public int currencyCostBaseRepair = 1;
    public int currenctCurrencyCostBaseRepair = 1;

    public int currencyCostInitialTurret = 30;
    public int currenctCurrencyCostInitialTurret = 30;

    public int currencyCostUpgradeTurret = 1;
    public int currenctCurrencyCostUpgradeTurret = 1;

    public int currencyCostInitialTurretSecond = 30;
    public int currenctCurrencyCostInitialTurretSecond = 30;

    public int currencyCostUpgradeTurretSecond = 1;
    public int currenctCurrencyCostUpgradeTurretSecond = 1;

    private void Awake()
    {
        UpdateUpgradeBaseHealthCost();
        UpdateBaseRepairCost();
        UpdateTurretUpgradeCost();
        UpdateTurretInitialCost();

        UpdateTurretInitialCostSecond();
        UpdateTurretUpgradeCostSecond();

    }
    void Start()
    {
        isTurretSecondBought = false;
        isTurretBought = false;
        stoneWall.SetActive(false);
        stonePillar.SetActive(false);
        woodenWall.SetActive(false);
        woodenPillar.SetActive(false);

        turretBuildButtonSecond.SetActive(false);
        turretUpgradeButtonSecond.SetActive(false);
        turretUpgradeButton.SetActive(false);
        turretSecond.SetActive(false);
        turret.SetActive(false);
        upgrade_UI.SetActive(false);
        UpdateUpgradeBaseHealthCost();
        UpdateBaseRepairCost();

        playerHealthManager = FindObjectOfType<PlayerHealthManager>();
        shieldManager = FindObjectOfType<ShieldManager>();
        baseHealthManager = FindObjectOfType<BaseHealthManager>();
        playerWeapon = FindObjectOfType<PlayerWeapon>();
        currencyManager = FindObjectOfType<CurrencyManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (upgrade_UI.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                BaseHealthUpgrade();
                UpdateUpgradeBaseHealthCost();

            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                BaseHealthRepair();
                UpdateBaseRepairCost();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                BaseTurretInnitial();
                UpdateTurretInitialCost();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && isTurretBought)
            {
                BaseTurretUpgrade();
                UpdateTurretUpgradeCost();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))

            {
                BaseTurretInnitialSecond();
                UpdateTurretInitialCostSecond();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && isTurretSecondBought)
            {
                BaseTurretUpgradeSecond();
                UpdateTurretUpgradeCostSecond();
            }

        }

    }

    public void BaseHealthUpgrade()
    {
        currencyCostBaseHealthUpgrade = 1 + currenctCurrencyCostBaseHealthUpgrade;

        if (currencyManager.currentCurrencyAmount >= currencyCostBaseHealthUpgrade)
        {

            currencyManager.RemoveCurrency(currencyCostBaseHealthUpgrade);
            currencyManager.UpdateCurrencyText();

            //Debug.Log(baseHealthManager.maxHealth);

            baseHealthManager.maxHealth += currencyCostBaseHealthUpgrade + 50f;


            baseHealthManager.UpdateBaseHealthBar();

            currenctCurrencyCostBaseHealthUpgrade += (7 * 5 / 3);
            UpdateUpgradeBaseHealthCost();
            //Debug.Log(currencyCostBaseHealthUpgrade);
            if (baseHealthManager.maxHealth >= 250)
            {
                if (isTurretBought && !isTurretSecondBought)
                {
                    turretBuildButtonSecond.SetActive(true);

                }
                woodenWall.SetActive(true);
                woodenPillar.SetActive(true);
            }
            if (baseHealthManager.maxHealth >= 500f)
            {
                woodenWall.SetActive(false);
                stoneWall.SetActive(true);
                stonePillar.SetActive(true);
            }
        }
    }

    public void BaseHealthRepair()
    {
        currencyCostBaseRepair = 1 + currenctCurrencyCostBaseRepair;

        if (currencyManager.currentCurrencyAmount >= currencyCostBaseRepair && baseHealthManager.currentHealth < baseHealthManager.maxHealth)
        {

            currencyManager.RemoveCurrency(currencyCostBaseRepair);
            currencyManager.UpdateCurrencyText();

            baseHealthManager.currentHealth = baseHealthManager.maxHealth;
            baseHealthManager.UpdateBaseHealthBar();
            currenctCurrencyCostBaseRepair += (12 * 3 / 3);

            UpdateBaseRepairCost();

            //Debug.Log(currencyCostBaseRepair);
        }
    }

    public void BaseTurretInnitial()
    {
        if (currencyManager.currentCurrencyAmount >= currencyCostInitialTurret)
        {
            isTurretBought = true;
            turret.SetActive(true);

            currencyManager.RemoveCurrency(currencyCostInitialTurret);
            currencyManager.UpdateCurrencyText();

            UpdateTurretUpgradeCost();
            turretBuildButton.SetActive(false);
            turretUpgradeButton.SetActive(true);

        }
        else
        {
            return;
        }
    }
    public void BaseTurretInnitialSecond()
    {
        if (currencyManager.currentCurrencyAmount >= currencyCostInitialTurretSecond && isTurretBought && baseHealthManager.maxHealth > 250)
        {
            isTurretSecondBought = true;
            turretSecond.SetActive(true);

            currencyManager.RemoveCurrency(currencyCostInitialTurretSecond);
            currencyManager.UpdateCurrencyText();

            UpdateTurretInitialCostSecond();
            turretBuildButtonSecond.SetActive(false);
            turretUpgradeButtonSecond.SetActive(true);

        }
        else
        {
            return;
        }


    }

    public void BaseTurretUpgrade()
    {
        currencyCostUpgradeTurret = 1 + currenctCurrencyCostUpgradeTurret;

        if (currencyManager.currentCurrencyAmount >= currencyCostUpgradeTurret)
        {

            currencyManager.RemoveCurrency(currencyCostUpgradeTurret);
            currencyManager.UpdateCurrencyText();



            arrowTurret.damage += 10;


            if (arrowTurret.firingForce.z + arrowTurret.firingForce.z * 0.05f > arrowTurret.MaxFiringForce.z)
            {
                arrowTurret.firingForce.z = arrowTurret.MaxFiringForce.z;
            }
            else
            {
                arrowTurret.firingForce += arrowTurret.firingForce * 0.05f;

            }
            currenctCurrencyCostUpgradeTurret += (7 * 6 / 3);

            UpdateTurretUpgradeCost();
        }
    }


    public void BaseTurretUpgradeSecond()
    {
        turretBuildButtonSecond.SetActive(false);
        currencyCostUpgradeTurretSecond = 1 + currencyCostUpgradeTurretSecond;

        if (currencyManager.currentCurrencyAmount >= currencyCostUpgradeTurretSecond)
        {

            currencyManager.RemoveCurrency(currencyCostUpgradeTurretSecond);
            currencyManager.UpdateCurrencyText();



            arrowTurret.damage += 10;


            if (arrowTurret.firingForce.z + arrowTurret.firingForce.z * 0.05f > arrowTurret.MaxFiringForce.z)
            {
                arrowTurret.firingForce.z = arrowTurret.MaxFiringForce.z;
            }
            else
            {
                arrowTurret.firingForce += arrowTurret.firingForce * 0.05f;

            }
            currenctCurrencyCostUpgradeTurretSecond += (7 * 6 / 3);

            UpdateTurretUpgradeCostSecond();
        }
    }



    public void PlayerDamageUpgrade()
    {
        playerWeapon.damage += 10;
    }
    public void PlayerShieldUpgrade()
    {
        shield.maxShield += 50;
        shieldManager.UpdateShieldBar();
    }




    public void UpgradeMenuToggle()
    {

        if (upgrade_UI.activeInHierarchy)
        {
            ResumeLevelUpgrade();
        }
        else
        {
            PauseLevelUpgrade();
        }
        if (upgrade_UIaudio != null) upgrade_UIaudio.Play();
        else Debug.LogError("respawn_UIaudio not found");
    }

    public void ResumeLevelUpgrade()
    {
        upgrade_UI.SetActive(false);
    }

    public void PauseLevelUpgrade()
    {
        upgrade_UI.SetActive(true);
    }


    public void UpdateUpgradeBaseHealthCost()
    {
        CostBaseHealthUpgradeText.text = currenctCurrencyCostBaseHealthUpgrade.ToString();
    }

    public void UpdateBaseRepairCost()
    {

        CostBaseRepairText.text = currenctCurrencyCostBaseRepair.ToString();
    }

    public void UpdateTurretUpgradeCost()
    {

        CostTurretUpgradeText.text = currenctCurrencyCostUpgradeTurret.ToString();
    }

    public void UpdateTurretInitialCost()
    {

        CostTurretInitialText.text = currencyCostInitialTurret.ToString();
    }



    public void UpdateTurretUpgradeCostSecond()
    {

        CostTurretUpgradeSecondText.text = currenctCurrencyCostUpgradeTurretSecond.ToString();
    }

    public void UpdateTurretInitialCostSecond()
    {

        CostTurretInitialSecondText.text = currencyCostInitialTurretSecond.ToString();
    }
}
