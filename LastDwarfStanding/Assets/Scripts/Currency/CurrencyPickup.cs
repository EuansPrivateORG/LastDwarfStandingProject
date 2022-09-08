using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyPickup : MonoBehaviour
{
    public int currencyValue;
    private CurrencyManager _currencyManager;
    private bool currencyPickedup;

    // Start is called before the first frame update
    void Start()
    {
        _currencyManager = FindObjectOfType<CurrencyManager>();
        currencyPickedup = false;

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !currencyPickedup)
        {
            currencyPickedup = true;
            //adding the value of the picked up currency to the currency value
            _currencyManager.AddCurrency(currencyValue);


            //destroys the currency object once it has been picked up and added to currencyAmount 
            Destroy(gameObject);
        }
    }

}


