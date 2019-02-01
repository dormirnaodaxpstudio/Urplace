using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarLuz : MonoBehaviour
{
    [SerializeField] Alavanca alavanca;
    [SerializeField] GameObject signToLuminate;

    // Update is called once per frame
    void Update()
    {   
        if (alavanca.isActivated)
        {
            signToLuminate.SetActive(true);
        }
    }
}
