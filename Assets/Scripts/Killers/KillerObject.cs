using UnityEngine;

public class KillerObject : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    private AudioSource deadNote;

    private void Awake()
    {
        deadNote = this.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PEGOU O PLAYER");
            if (!deadNote.isPlaying)
                deadNote.Play();
            playerInputHandler.playerIsDead = true;
        }
    }
}
