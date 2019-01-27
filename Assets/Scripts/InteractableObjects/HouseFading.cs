using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseFading : MonoBehaviour
{
    [SerializeField] Animator houseFadingAnimator;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            houseFadingAnimator.enabled = true;
        }
    }
}
