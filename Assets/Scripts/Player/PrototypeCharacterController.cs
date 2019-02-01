using UnityEngine;
using System.Collections;

public class PrototypeCharacterController : MonoBehaviour
{

    public float speed = 10f;
    public float jumpSpeed = 10f;
    public Vector3 extraGravity = Vector3.zero;

    private Rigidbody _rigidBody;
    private Collider _collider;
    private Vector3 moveDirection = Vector3.zero;
    private bool isGrounded;

    void Awake()
    {
        _collider = GetComponent<Collider>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        CheckGroundedStatus();

        //_rigidBody.useGravity = !isGrounded;

        float horizontalAxisValue = Input.GetAxisRaw("Horizontal");

        moveDirection = speed * horizontalAxisValue * Vector3.right;
        moveDirection.y = _rigidBody.velocity.y;

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            moveDirection.y += jumpSpeed;      

        _rigidBody.velocity = moveDirection;
        _rigidBody.AddForce(extraGravity);
    }

    void CheckGroundedStatus()
    {
        float groundDistance = _collider.bounds.extents.y;
        isGrounded = Physics.Raycast(transform.position, -transform.up, groundDistance + 0.15f);
    }

    //void OnCollisionEnter()
    //{
    //    isGrounded = true;
    //}

    //void OnCollisionStay()
    //{
    //    isGrounded = true;
    //}

    //void OnCollisionExit()
    //{
    //    isGrounded = false;
    //}

}
