using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseCollider : MonoBehaviour
{
    [SerializeField] PlayerInputHandler playerInputHandler;
    [SerializeField] Animator houseAnimation;
    [SerializeField] GameObject thankYouForPlayingPanel;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInputHandler._velocity.x = 0f;
            playerInputHandler.canMove = false;
            houseAnimation.enabled = true;
            StartCoroutine(WaitToRestart());
        }
    }

    public IEnumerator WaitToRestart()
    {
        yield return new WaitForSeconds(5f);
        thankYouForPlayingPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        playerInputHandler.transform.Translate(new Vector3(-7.26f, -0.08f, 0f));
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Game");
    }
}