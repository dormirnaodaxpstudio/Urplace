using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] private RespawnManager respawnManager;

    public void SetTeleport()
    {
        respawnManager.CallTeleport();
    }
}