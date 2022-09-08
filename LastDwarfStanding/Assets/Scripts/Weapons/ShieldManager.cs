using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class ShieldManager : MonoBehaviour
{
    public bool shieldActive = false;
    public Transform shieldPosition;
    private PlayerNavigationManager _playerNavigationManager;
    private PauseGame _pauseGame;


    public Shield shield;


    public Slider shieldSlider;


    private PlayerSoundManager _playerSoundManager;


    private void Awake()
    {
        shieldSlider.maxValue = shield.maxShield;
        UpdateShieldBar();

        _playerNavigationManager = FindObjectOfType<PlayerNavigationManager>();
        _playerSoundManager = GetComponent<PlayerSoundManager>();
        _pauseGame = FindObjectOfType<PauseGame>();
        if (shield == null) shield = GetComponentInChildren<Shield>();
    }
    
        
            
    void Start()
    {

        _playerNavigationManager.isControllerActive = true;

        if (shield == null) Debug.LogError(name + " has no shield variable");
    }
        
    

    // Update is called once per frame
    void Update()
    {
        ShieldAction();
        IsShieldBroken();
    }

    public void ShieldAction()
    {
        if (!_playerNavigationManager.isControllerActive) return;

        if (Input.GetButtonDown("Fire2") && UnityEngine.EventSystems.EventSystem.current != null &&
            !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && shield.currentShield > 0)
        {
            shield.gameObject.SetActive(true);
            shieldActive = true;
            UpdateShieldBar();
            //Debug.Log("Right Mouse Pressed Shield UP");
        }
        if (Input.GetButtonUp("Fire2") && UnityEngine.EventSystems.EventSystem.current != null &&
        !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            shield.gameObject.SetActive(false);
            shieldActive = false;
            //Debug.Log("Right Mouse let go Shield Down");
        }
    }


    public void UpdateShieldBar()
    {
        shieldSlider.value = shield.currentShield;
    }

    public void IsShieldBroken()
    {
        if(shieldActive && shield.currentShield <= 0)
        {
            shield.gameObject.SetActive(false);
            shieldActive = false;
        }
    }
}



