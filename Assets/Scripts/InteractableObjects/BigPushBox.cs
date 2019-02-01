using UnityEngine;

public class BigPushBox : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;

    private void Update()
    {
        if (this.transform.position.x >= 428.6f)
        {
            playerInputHandler.pushGripActive = false;
            this.transform.SetParent(null);
            this.gameObject.layer = 0;
        }
    }
}