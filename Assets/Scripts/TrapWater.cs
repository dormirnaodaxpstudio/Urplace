using UnityEngine;

public class TrapWater : MonoBehaviour
{
    [SerializeField] private GameObject water;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            water.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}