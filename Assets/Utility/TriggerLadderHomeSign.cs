using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLadderHomeSign : MonoBehaviour
{
    [SerializeField] Animator homeSignAnimator;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            homeSignAnimator.enabled = true;
        }        
    }
}
