using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowersCollider : MonoBehaviour
{
    [SerializeField] GameObject flowersTree;
    private Animator flowersAnimator;
    private AudioSource soundEffect;

    private void Awake()
    {
        flowersAnimator = flowersTree.GetComponent<Animator>();
        soundEffect = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            soundEffect.Play();
            flowersAnimator.enabled = true;
        }
    }
}
