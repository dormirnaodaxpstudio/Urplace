using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> listOfRespawns;
    [SerializeField] private GameObject character;

    [SerializeField] private Animator blackFadingAnimation;

    private PlayerInputHandler playerInputHandler;

    void Start()
    {
        playerInputHandler = character.GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInputHandler.playerIsDead)
        {
            TeleportToRespawnPoint();
        }
            
    }
    public void AddRespawnPoint(GameObject newRespawnPoint)
    {
        if (!listOfRespawns.Contains(newRespawnPoint))
            listOfRespawns.Add(newRespawnPoint);
    }

    public void TeleportToRespawnPoint ()
    {
        character.transform.position = listOfRespawns[listOfRespawns.Count -1].transform.position;
        playerInputHandler.playerIsDead = false;
    }
}
