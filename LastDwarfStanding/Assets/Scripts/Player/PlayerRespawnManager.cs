using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PlayerRespawnManager : MonoBehaviour
{
    public Text RespawnPopupText;

    private Animator _reviveAnimation;
    private PlayerHealthManager _playerHealthManager;

    // Start is called before the first frame update
    void Start()
    {
        _playerHealthManager = FindObjectOfType<PlayerHealthManager>();
        _reviveAnimation = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerHealthManager.OndeathActive)
        {
            //Debug.Log("Playing respawn text");
           _reviveAnimation.SetTrigger("TriggerRespawnAnimation");

        }
    }
}
