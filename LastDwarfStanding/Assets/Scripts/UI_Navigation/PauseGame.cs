using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseGame : MonoBehaviour
{

    public GameObject escape_Button;
    public GameObject pause_UI;
    public GameObject transparentBackground;
    private PlayerNavigationManager _playerNavigationManager;
    private EnemyNavigationManager[] _enemyNavigationManager;
    private Arrow[] _arrows;
    public AudioSource scrollUIAudio;

    private void Awake()
    {
        _playerNavigationManager =FindObjectOfType<PlayerNavigationManager>();
        _enemyNavigationManager =FindObjectsOfType<EnemyNavigationManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

        pause_UI.SetActive(false);
        transparentBackground.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuToggle();
        }

        _enemyNavigationManager = FindObjectsOfType<EnemyNavigationManager>();

    }

    public void ResumeLevel()
    {
        pause_UI.SetActive(false);

        transparentBackground.SetActive(false);
        _playerNavigationManager.isControllerActive = true;
        
        foreach (EnemyNavigationManager enemy in _enemyNavigationManager) 
        {
            enemy.isEnemyActive = true;
        }
        


    }

    public void PauseLevel()
    {
        pause_UI.SetActive(true);

        transparentBackground.SetActive(true);
        _playerNavigationManager.isControllerActive = false;
    

        foreach (EnemyNavigationManager enemy in _enemyNavigationManager)
        {
            enemy.isEnemyActive = false;
            Debug.Log("Enemy is not active" + name);
            enemy.EnemyOnPause();
        
        }
       /* foreach (Arrow arrow in _arrows)
            arrow.CanFire = false;
        Arrow.ArrowOnPause();

        */

    }

    public void PauseMenuToggle()
    {
        if (pause_UI.activeInHierarchy)
        {
            ResumeLevel();
        }
        else
        {
            PauseLevel();
        }
        if (scrollUIAudio != null) scrollUIAudio.Play();
        else Debug.LogError("ScrollUIAudio not found");

    }

    public bool PauseMenuActive()
    {
        return pause_UI.activeInHierarchy;

    }
}