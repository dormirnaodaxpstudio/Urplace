using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] Character character;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            character.isGrounded = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            character.isGrounded = false;
        }
    }
}
