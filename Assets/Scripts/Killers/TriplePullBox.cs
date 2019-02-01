using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriplePullBox : MonoBehaviour
{
    [SerializeField] PlayerInputHandler playerInputHandler;

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x <= 328.5f)
        {
            this.transform.position = new Vector3(328.5f, this.transform.position.y, this.transform.position.z);
            playerInputHandler.pullGripActive = false;
            this.transform.SetParent(null);
            this.gameObject.layer = 0;
        }
            
    }
}
