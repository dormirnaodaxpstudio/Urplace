using UnityEngine;

public class TrapWater : MonoBehaviour
{
    [SerializeField] private GameObject water;
    [SerializeField] private Animation anim;
    [SerializeField] private AudioSource waterGoingUpSound;

    private Vector3 posInitial;

    private void Start()
    {
        posInitial = water.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.GetComponent<Collider>().enabled = false;
            water.SetActive(true);
            anim.Play();
            waterGoingUpSound.Play();
        }
    }

    public void Restart()
    {
        this.GetComponent<Collider>().enabled = true;
        anim.Stop();
        water.transform.position = posInitial;
        water.SetActive(false);
    }
}