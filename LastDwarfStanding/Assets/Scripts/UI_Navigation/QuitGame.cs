using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{

    public GameObject Quit_Button;
    // Start is called before the first frame update
    void Start()
    {
        Quit_Button.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
        public void QuitGameFunction()
        {
            //print("QUITTING");
            Application.Quit(); 
        }
    public void QuitToMenuFunction()
    {
        //print("QUITTING TO MENU");
        SceneManager.LoadScene("TitleScene");
    }
}
