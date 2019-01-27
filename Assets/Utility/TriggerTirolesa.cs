using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTirolesa : MonoBehaviour
{
    [SerializeField] PlayerInputHandler playerInputHandler;
    Animator tirolesaAnimation;
    [SerializeField] Animator charTirolesaAnimation; 

    void Awake()
    {
        tirolesaAnimation = this.GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag("Player"))
        {
            playerInputHandler.tirolesaActive = true;
            tirolesaAnimation.enabled = true;
            charTirolesaAnimation.enabled = true;
        }
        
    }
}
