using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
