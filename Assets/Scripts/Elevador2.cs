using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevador2 : MonoBehaviour
{
    [SerializeField] private Alavanca alavanca1;
    [SerializeField] private Alavanca alavanca2;
    [SerializeField] private Alavanca alavanca3;

    [SerializeField] Animator elevador2Animation;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (alavanca1.isActivated && alavanca2.isActivated && alavanca3.isActivated)
            {
                elevador2Animation.enabled = true;
            }
        }
    }
}
