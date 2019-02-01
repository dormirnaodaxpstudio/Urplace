using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLadderHomeSign : MonoBehaviour
{
    [SerializeField] Animator homeSignAnimator;
    private AudioSource homeSignAudio;

    private void Awake()
    {
        homeSignAudio = this.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            homeSignAnimator.enabled = true;
            homeSignAudio.Play();
        }        
    }
}
