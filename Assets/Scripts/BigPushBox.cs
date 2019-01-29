using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPushBox : MonoBehaviour
{
    [SerializeField] PlayerInputHandler playerInputHandler;

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x >= 428.6f)
        {
            playerInputHandler.pushGripActive = false;
            this.transform.SetParent(null);
            this.gameObject.layer = 0;
        }

    }
}
