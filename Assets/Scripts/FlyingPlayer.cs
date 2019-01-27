#define DEBUGMODE
using UnityEngine;
using System.Collections;

public class FlyingPlayer : MonoBehaviour
{

    public float playerSpeed = 10f;
    public float airSpeed = 20f;
    public float jumpHeight = 3f;
    public float groundDamping = 10f;
    public float airDamping = 5f;
    public Vector3 playerGravity;

    private Vector3 _velocity;
    private PrototypeCharacterControllerv2 _controller;

#if DEBUGMODE
    private Renderer _renderer;
#endif


    // Use this for initialization
    void Awake()
    {
        _controller = GetComponent<PrototypeCharacterControllerv2>();

#if DEBUGMODE
        _renderer = GetComponent<Renderer>();
#endif
    }

    void Update()
    {
        _velocity = _controller.velocity;

#if DEBUGMODE
        _renderer.material.color = _controller.isGrounded ? Color.green : Color.red;
#endif

        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        float movementDamping = _controller.isGrounded ? groundDamping : airDamping;
        if(verticalAxis != 0)
            _velocity.y = Mathf.Lerp(_velocity.y, verticalAxis * airSpeed, Time.deltaTime * movementDamping);
        _velocity.x = Mathf.Lerp(_velocity.x, horizontalAxis * playerSpeed, Time.deltaTime * movementDamping);

        _velocity += playerGravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

}
