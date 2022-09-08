using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{

    public GameObject Play_Button;
    // Start is called before the first frame update
    void Start()
    {
        Play_Button.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void StartLvl()
    {
    SceneManager.LoadScene("MainScene");

    }

}