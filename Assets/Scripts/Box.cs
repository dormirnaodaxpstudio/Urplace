using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Vector3 posInitial;

    private void Start()
    {
        posInitial = this.transform.position;
    }

    public void Restart()
    {
        this.transform.position = posInitial;
    }
}
