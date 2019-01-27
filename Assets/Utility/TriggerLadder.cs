using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLadder : MonoBehaviour
{
    [SerializeField] GameObject gameObjectToDestroy;
    [SerializeField] GameObject gameObjectToSpawn;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObjectToDestroy.SetActive(false);
            gameObjectToSpawn.SetActive(true);
        }
    }
}
