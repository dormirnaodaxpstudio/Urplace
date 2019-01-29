using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverWallCollider : MonoBehaviour
{
    [SerializeField] private Animator leverWallAnimation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            leverWallAnimation.enabled = true;
        }
    }
}
