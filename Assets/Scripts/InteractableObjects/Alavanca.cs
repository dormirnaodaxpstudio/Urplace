using UnityEngine;

public class Alavanca : MonoBehaviour
{
    Animator alavancaAnimation;
    AudioSource leverSound;
    public bool isActivated;

    void Awake()
    {
        alavancaAnimation = this.GetComponent<Animator>();
        leverSound = this.GetComponent<AudioSource>();
        isActivated = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                alavancaAnimation.enabled = true;
                leverSound.Play();
                isActivated = true;
            }
        }
    }
}
