using UnityEngine;

public class TrapWater : MonoBehaviour
{
    [SerializeField] private GameObject water;
    [SerializeField] private Animation anim;

    private Vector3 posInitial;

    private void Start()
    {
        posInitial = water.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            water.SetActive(true);
            anim.Play();
            this.GetComponent<Collider>().enabled = false;
        }
    }

    public void Restart()
    {
        anim.Stop();
        water.transform.position = posInitial;
        water.SetActive(false);
        this.GetComponent<Collider>().enabled = true;
    }
}