using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTirolesa : MonoBehaviour
{
    [SerializeField] PlayerInputHandler playerInputHandler;
    Animator tirolesaAnimation;
    [SerializeField] Animator charTirolesaAnimation;
    AudioSource ziplineSound;

    void Awake()
    {
        tirolesaAnimation = this.GetComponent<Animator>();
        ziplineSound = this.GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag("Player"))
        {
            playerInputHandler.tirolesaActive = true;
            tirolesaAnimation.enabled = true;
            charTirolesaAnimation.enabled = true;
            ziplineSound.Play();
        }
        
    }
}
