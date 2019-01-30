using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esteira : MonoBehaviour
{
    [SerializeField] Alavanca alavanca;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (alavanca.isActivated)
        {
            this.GetComponent<Animator>().enabled = true;
            this.GetComponent<AudioSource>().enabled = true;
        }
            
    }
}
