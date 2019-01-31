using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> listOfRespawns;
    [SerializeField] private List<GameObject> objectsRestart;
    [SerializeField] private GameObject character;

    [SerializeField] private Animation anim;

    private PlayerInputHandler playerInputHandler;

    private void Start()
    {
        playerInputHandler = character.GetComponent<PlayerInputHandler>();
    }

    public void Restart()
    {
        foreach (GameObject obj in objectsRestart)
        {
			if (obj != null)
            {
				obj.SendMessage("Restart");
			}
		}
    }

    private void Update()
    {
        if (playerInputHandler.playerIsDead)
        {
            CallDeadAnimation();
        }
    }

    public void AddRespawnPoint(GameObject newRespawnPoint)
    {
        if (!listOfRespawns.Contains(newRespawnPoint))
            listOfRespawns.Add(newRespawnPoint);
    }

    public void CallDeadAnimation()
    {
        anim.Play();
    }

    public void CallTeleport()
    {
        this.Restart();
        playerInputHandler.playerIsDead = false;
        character.transform.position = listOfRespawns[listOfRespawns.Count -1].transform.position;
    }
}