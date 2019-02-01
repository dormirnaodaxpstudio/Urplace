using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLadderType2 : MonoBehaviour
{    [SerializeField] GameObject gameObjectToSpawn;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObjectToSpawn.SetActive(true);
        }
    }
}
