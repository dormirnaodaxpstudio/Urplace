using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseFading : MonoBehaviour
{
    [SerializeField] Animator houseFadingAnimator;

    private AudioSource vanishSound;

    private void Awake()
    {
        vanishSound = this.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            houseFadingAnimator.enabled = true;
            vanishSound.Play();
        }
    }
}
