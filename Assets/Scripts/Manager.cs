using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private GameObject tutorialsTexts;

    private void Awake()
    {
        playerInputHandler.canMove = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jogo iniciado");
            playerInputHandler.canMove = true;
            menu.SetActive(false);
            tutorialsTexts.SetActive(true);

        }
    }

    public void Exit()
    {
        #if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
    }
}