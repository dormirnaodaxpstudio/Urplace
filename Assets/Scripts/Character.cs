using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float speedMovement;

    private void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        this.transform.position += move * speedMovement * Time.deltaTime;
    }
}