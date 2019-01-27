using UnityEngine;

public class KillerObject : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PEGOU O PLAYER");
            playerInputHandler.playerIsDead = true;
        }
    }
}
