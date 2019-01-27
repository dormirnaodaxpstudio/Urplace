using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseCollider : MonoBehaviour
{
    [SerializeField] PlayerInputHandler playerInputHandler;
    [SerializeField] Animator houseAnimation;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInputHandler.canMove = false;
            houseAnimation.enabled = true;
            StartCoroutine(WaitToRestart());

        }        
    }

    public IEnumerator WaitToRestart()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}
