using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float speedMovement;
    [SerializeField] private float jumpForce;

    private Rigidbody charRb; //rigidbody do personagem
    private Collider c; //Collider do personagem
    private float distToGround;

    void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
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
        if (Input.GetMouseButtonDown(0))
        {
            //charRb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
            //GetComponent<Rigidbody>().velocity.y = jumpForce;
            charRb.velocity += jumpForce * Vector3.up;
        }
    }
}