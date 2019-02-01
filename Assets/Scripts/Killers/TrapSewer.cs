using UnityEngine;

public class TrapSewer : MonoBehaviour
{
    [SerializeField] private GameObject trap;
    private PlayerInputHandler player;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.GetComponent<Collider>().enabled = false;
            trap.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public void Restart()
    {
        this.GetComponent<Collider>().enabled = true;
    }
}