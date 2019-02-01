using UnityEngine;

public class PressButtons : MonoBehaviour
{
	[SerializeField] private GameObject back;
	[SerializeField] private GameObject actual;

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Escape)) {
            back.SetActive(true);
            actual.SetActive(false);
        }
	}
}