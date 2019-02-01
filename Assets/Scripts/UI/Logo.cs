using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour
{
	public GameObject nextLogo;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

	public void SwitchLogo()
    {
		this.gameObject.SetActive(false);
		nextLogo.SetActive(true);
	}
	
	public void GoGame()
    {
		SceneManager.LoadScene("Game");
	}
}