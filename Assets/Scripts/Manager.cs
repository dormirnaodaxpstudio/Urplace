using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] PlayerInputHandler playerInputHandler;
    [SerializeField] private GameObject tutorialsTexts;

    private void Awake()
    {
        //Time.timeScale = 0;
        playerInputHandler.canMove = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jogo iniciado");
            //Time.timeScale = 1;
            playerInputHandler.canMove = true;
            menu.SetActive(false);
            tutorialsTexts.SetActive(true);

        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
