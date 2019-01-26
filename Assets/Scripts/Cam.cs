using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Character character;

    private void Update()
    {
        this.Move();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor")) {
            
        }
    }

    private void Move()
    {
        this.transform.position = Vector3.Slerp(this.transform.position, new Vector3(character.transform.position.x, character.transform.position.y + 2.2f, this.transform.position.z), speed);
    }
}