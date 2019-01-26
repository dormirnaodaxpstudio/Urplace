using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBox : MonoBehaviour
{
    [SerializeField] private GameObject frontCollider;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.K))
            frontCollider.SetActive(true);

        if (Input.GetKeyUp(KeyCode.K))
            frontCollider.SetActive(false);
    }
}
