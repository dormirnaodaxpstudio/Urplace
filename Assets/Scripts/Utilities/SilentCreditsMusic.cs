using UnityEngine;
using UnityEngine.SceneManagement;

public class SilentCreditsMusic : MonoBehaviour
{
    // [SerializeField] private float speedCredits;
    // [SerializeField] private GameObject credits;

    [SerializeField] private AudioSource endPianoMusic1;
    [SerializeField] private AudioSource endPianoMusic2;

    /* Alternative */
    // private void Update()
    // {
    //     credits.transform.position += credits.transform.up * Time.deltaTime * speedCredits;
    // }

    public void BackMenu()
    {
        SceneManager.LoadScene("Game");
    }

    public void SilentMusic()
    {
        endPianoMusic1.volume -= 0.25f;
        endPianoMusic2.volume -= 0.25f;
    }
}