using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioImpactCollider : MonoBehaviour
{
    private AudioSource impactSound;

    private void Awake()
    {
        impactSound = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            impactSound.Play();
        }
    }
}
