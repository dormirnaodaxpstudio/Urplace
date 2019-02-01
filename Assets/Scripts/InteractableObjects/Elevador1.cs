using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevador1 : MonoBehaviour
{
    [SerializeField] private Alavanca alavanca;

    [SerializeField] Animator elevador1Animation;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (alavanca.isActivated)
            {
                elevador1Animation.enabled = true;
            }
        }
    }
}
