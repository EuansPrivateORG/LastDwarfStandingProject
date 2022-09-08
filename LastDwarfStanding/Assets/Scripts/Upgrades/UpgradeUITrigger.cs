using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUITrigger : MonoBehaviour
{

    private UpgradeManager upgradeManager;
    private PassiveIncomeFarmManager passiveIncomeFarmManager;
    public GameObject upgrade_UI;
    // Start is called before the first frame update
    void Start()
    {
        upgradeManager = FindObjectOfType<UpgradeManager>();
        passiveIncomeFarmManager = GetComponent<PassiveIncomeFarmManager>();


    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {

            //upgradeManager.PauseLevelUpgrade();
            if (passiveIncomeFarmManager != null)
            {
                passiveIncomeFarmManager.farmUpgradeUI.PauseLeveFarm();

            }
            else if (upgradeManager != null)
            {
                upgradeManager.PauseLevelUpgrade();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (passiveIncomeFarmManager != null)
            {

                passiveIncomeFarmManager.farmUpgradeUI.ResumeLevelFarm();

            }
            else if (upgradeManager != null)
            {
                upgradeManager.ResumeLevelUpgrade();
            }

        }
    }
}
