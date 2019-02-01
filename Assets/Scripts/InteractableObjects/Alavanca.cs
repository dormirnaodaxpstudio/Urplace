using UnityEngine;

public class Alavanca : MonoBehaviour
{
    Animator alavancaAnimation;
    public bool isActivated;

    void Awake()
    {
        alavancaAnimation = this.GetComponent<Animator>();
        isActivated = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                alavancaAnimation.enabled = true;
                isActivated = true;
            }
        }
    }
}
