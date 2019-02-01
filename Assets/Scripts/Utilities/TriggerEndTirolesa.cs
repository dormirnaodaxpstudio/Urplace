using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEndTirolesa : MonoBehaviour
{   
    [SerializeField] Animator tirolesaAnimation;
    [SerializeField] Animator charTirolesaAnimation; 
    [SerializeField] PlayerInputHandler playerInputHandler;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("INTERAGI PORRA!");
            playerInputHandler.tirolesaActive = false;
            tirolesaAnimation.enabled = false;
            charTirolesaAnimation.enabled = false;
        }
    }
}
