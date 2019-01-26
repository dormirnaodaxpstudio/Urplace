using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBox : MonoBehaviour
{
    //[SerializeField] private GameObject frontCollider;
    Collider[] hitColliders;
    public bool gripActive;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.K))
        {
            //frontCollider.SetActive(true);
            hitColliders = Physics.OverlapBox(this.transform.position + transform.forward, Vector3.one * 1f);
            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.CompareTag("Box"))
                {
                    gripActive = true;
                    collider.gameObject.transform.SetParent(this.transform);
                    collider.gameObject.layer = 9; //Layer MoveableObject
                }
            }
        }
            
        else if (Input.GetKeyUp(KeyCode.K))
        {
            //frontCollider.SetActive(false);
            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.CompareTag("Box"))
                {
                    gripActive = false;
                    collider.gameObject.transform.SetParent(null);
                    collider.gameObject.layer = 0; //Layer Default
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position + transform.forward, Vector3.one * 2f);
    }
}
