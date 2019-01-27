using System.Collections.Generic;

using UnityEngine;

public class Trap : MonoBehaviour
{
    bool active;
    private Vector3 posInitial;

    private void Start()
    {
        posInitial = this.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.GetComponent<Collider>().isTrigger = true;
        }

        if (other.CompareTag("Floor"))
        {
            this.GetComponent<Collider>().isTrigger = false;
        }
    }

    public void Restart()
    {
        this.transform.position = posInitial;
        this.GetComponent<Collider>().isTrigger = false;
        this.GetComponent<Rigidbody>().useGravity = false;
    }
}