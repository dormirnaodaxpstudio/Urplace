using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableInputs : MonoBehaviour
{
    [SerializeField] GameObject character;
    PlayerInputHandler playerInputHandler;

    private void Awake()
    {
        playerInputHandler = character.GetComponent<PlayerInputHandler>();
    }

    public void EnableControllerInputs()
    {
       playerInputHandler.enabled = true;
       playerInputHandler.playerSpeed = 8f;
    }

    public void DisableControllerInputs()
    {
        playerInputHandler._velocity.x = 0f;
        playerInputHandler._velocity.y = 0f;
        playerInputHandler.enabled = false;
    }
}
