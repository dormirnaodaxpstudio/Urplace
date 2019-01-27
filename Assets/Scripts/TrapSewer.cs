using UnityEngine;

public class TrapSewer : MonoBehaviour
{
    [SerializeField] private GameObject trap;
    private PlayerInputHandler player;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trap.GetComponent<Rigidbody>().useGravity = true;
            this.gameObject.SetActive(false);
        }
    }
}
