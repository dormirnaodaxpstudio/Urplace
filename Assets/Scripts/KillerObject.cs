using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerObject : MonoBehaviour
{
    [SerializeField] private GameObject character;
    private PlayerInputHandler playerInputHandler;

    void Awake()
    {
        playerInputHandler = character.GetComponent<PlayerInputHandler>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInputHandler.playerIsDead = true;
        }
    }
}
