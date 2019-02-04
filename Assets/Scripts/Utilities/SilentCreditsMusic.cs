using UnityEngine;
using UnityEngine.SceneManagement;

public class SilentCreditsMusic : MonoBehaviour
{
    [SerializeField] private AudioSource endPianoMusic1;
    [SerializeField] private AudioSource endPianoMusic2;

    public void SilentMusic()
    {
        endPianoMusic1.volume -= 0.25f;
        endPianoMusic2.volume -= 0.25f;
    }
}