using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAnimationCollider : MonoBehaviour
{
    [SerializeField] Animator birdAnimator;
    [SerializeField] AudioSource birdSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            birdAnimator.enabled = true;
            birdSound.enabled = true;
        }
    }
}
