using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private Transform character;
    [SerializeField] private bool onGrip;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            other.transform.SetParent(character);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            other.transform.SetParent(null);
        }
    }
}