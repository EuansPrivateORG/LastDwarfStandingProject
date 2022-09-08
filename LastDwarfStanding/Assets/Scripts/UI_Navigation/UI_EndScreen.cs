using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EndScreen : MonoBehaviour
{
    public Text waveCountText;
    
    // Start is called before the first frame update
    void Start()
    {
        SetWaveCountText();
    }

    private void SetWaveCountText()
    {
        SavingSystem savingSystem = FindObjectOfType<SavingSystem>();
        if (savingSystem == null) print("saving system not laoded");
        Dictionary<string, object> loadedState = new Dictionary<string, object>();
        object loadedObject = savingSystem.EndGameLoad();
        loadedState = (Dictionary<string, object>)loadedObject;

        int waveCount = 0;
        foreach (KeyValuePair<string, object> pair in loadedState)
        {
            if (pair.Key == "waveCount")
            {
                waveCount = (int)pair.Value;
                //print("found value = " + waveCount);
            }
        }

        if (waveCount != 0)
        {
            waveCountText.text = waveCount.ToString();
        }
          
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
