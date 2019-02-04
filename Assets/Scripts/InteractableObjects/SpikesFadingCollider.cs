using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesFadingCollider : MonoBehaviour
{
    [SerializeField] GameObject spikes;
    private Animator spikesAnimator;
    private AudioSource soundEffect;

    private void Awake()
    {
        spikesAnimator = spikes.GetComponent<Animator>();
        soundEffect = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            soundEffect.Play();
            spikesAnimator.enabled = true;
        }
    }
}
