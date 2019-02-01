using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject tutorialsTexts;

    [SerializeField] private PlayerInputHandler playerInputHandler;

    [SerializeField] private EnableDisableInputs enableDisableInputs;

    private void Awake()
    {
        this.ShowMouse(true);
        playerInputHandler.canMove = false;
        enableDisableInputs.DisableControllerInputs();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jogo iniciado");
            enableDisableInputs.EnableControllerInputs();
            playerInputHandler.canMove = true;
            menu.SetActive(false);
            pause.SetActive(false);
            tutorialsTexts.SetActive(true);
            this.ShowMouse(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !this.pause.activeInHierarchy && !this.menu.activeInHierarchy)
        {
            this.ShowMouse(true);
            this.pause.SetActive(true);
            enableDisableInputs.DisableControllerInputs();
        }
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
        Application.Quit();
        #elif UNITY_WEBGL
        Application.OpenURL("about:blank");
        #endif
    }

    public void ShowMouse(bool active)
    {
        if (active)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}