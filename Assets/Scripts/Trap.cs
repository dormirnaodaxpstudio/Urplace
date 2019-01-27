using System.Collections.Generic;

using UnityEngine;

public class Trap : MonoBehaviour
{
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
}