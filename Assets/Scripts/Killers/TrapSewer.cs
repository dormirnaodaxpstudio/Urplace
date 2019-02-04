using UnityEngine;

public class TrapSewer : MonoBehaviour
{
    [SerializeField] private GameObject trap;
    private PlayerInputHandler player;
    private AudioSource trapFallingSound;

    private void Awake()
    {
        trapFallingSound = this.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trapFallingSound.Play();
            this.GetComponent<Collider>().enabled = false;
            trap.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public void Restart()
    {
        this.GetComponent<Collider>().enabled = true;
    }
}