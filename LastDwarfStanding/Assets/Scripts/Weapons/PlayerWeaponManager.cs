using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class PlayerWeaponManager : MonoBehaviour
{

    public Transform weaponPosition;
    public GameObject weapon;
    public float swingDelay;
    private PlayerNavigationManager _playerNavigationManager;
    private PauseGame _pauseGame;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private PlayerSoundManager _playerSoundManager;

    private ShieldManager _shieldManager;
    private Shield _shield;

    private float _swingTimer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _playerNavigationManager = FindObjectOfType<PlayerNavigationManager>();
        _playerSoundManager = GetComponent<PlayerSoundManager>();
        _pauseGame = FindObjectOfType<PauseGame>();
        _shieldManager = GetComponent<ShieldManager>();

    }
    // Start is called before the first frame update
    void Start()
    {
        _playerNavigationManager.isControllerActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerNavigationManager.isControllerActive) return;

        _swingTimer += Time.deltaTime;

        if (_swingTimer >= swingDelay)
        {
            if (Input.GetButtonDown("Fire1") && !_shieldManager.shieldActive && UnityEngine.EventSystems.EventSystem.current != null &&
            !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                _playerNavigationManager.isControllerActive = false;
                GetComponentInChildren<PlayerWeapon>().didDamage = false;
                animator.SetTrigger("Attack");
                //Debug.Log("Attacking");
                weapon.GetComponent<Animator>().SetTrigger("Swing");
                //Debug.Log("Left Mouse Pressed weapon Swing");
                _swingTimer = 0;
                _playerNavigationManager.isControllerActive = true;
                _playerSoundManager.PlayAudioClip("Swoosh");


            }

        }

        }
    }



