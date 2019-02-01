using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Audio;

public class Trap : MonoBehaviour
{
    private bool active;
    
    private Vector3 posInitial;

    //[SerializeField] private AudioSource impactSFX;

    private void Start()
    {
        posInitial = this.transform.position;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.GetComponent<Collider>().isTrigger = true;
        }

        if (other.CompareTag("Floor"))
        {
            this.GetComponent<Collider>().isTrigger = false;
            this.AudioImpact();
        }
    }*/

    public void Restart()
    {
        this.transform.position = posInitial;
        this.GetComponent<Collider>().isTrigger = false;
        this.GetComponent<Rigidbody>().useGravity = false;
    }

    /* public void AudioImpact()
    {
        impactSFX.Play();
    }*/
}