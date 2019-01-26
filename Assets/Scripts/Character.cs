using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float speedMovement;
    [SerializeField] private float jumpForce;

    Rigidbody charRb; //rigidbody do personagem
    public bool isGrounded {get; set;}

    void Start()
    {
        charRb = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        this.transform.position += move * speedMovement * Time.deltaTime;
    }

    void Jump()
    {
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            // charRb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
            charRb.velocity += jumpForce * Vector3.up;
        }
    }
}