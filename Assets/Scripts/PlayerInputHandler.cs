#define DEBUGMODE
using UnityEngine;
using System.Collections;

public class PlayerInputHandler : MonoBehaviour
{

    public float playerSpeed = 20f;
    public float jumpHeight = 3f;
    public float groundDamping = 10f;
    public float airDamping = 5f;
    public Vector3 playerGravity;

    private Vector3 _velocity;
    private Vector3 moveDirection;
    private PrototypeCharacterControllerv2 _controller;

    TestBox testBox;

#if DEBUGMODE
    private Renderer _renderer;
#endif


    // Use this for initialization
    void Awake()
    {
        testBox = this.GetComponent<TestBox>();

        _controller = GetComponent<PrototypeCharacterControllerv2>();

#if DEBUGMODE
        _renderer = GetComponent<Renderer>();
#endif
    }

    void Update()
    {
        _velocity = _controller.velocity;

        Time.timeScale = Input.GetKey(KeyCode.Space) ? 0.1f : 1f;

#if DEBUGMODE
        _renderer.material.color = _controller.isGrounded ? Color.green : Color.red;
#endif

        float horizontalAxis = Input.GetAxis("Horizontal");

        bool jumpKeys = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);// || Input.GetKeyDown(KeyCode.Space);
        if (jumpKeys && _controller.isGrounded && !testBox.gripActive)
            _velocity.y = Mathf.Sqrt(2f * jumpHeight * -playerGravity.y);

        float movementDamping = _controller.isGrounded ? groundDamping : airDamping;

        _velocity.x = Mathf.Lerp(_velocity.x, horizontalAxis * playerSpeed, Time.deltaTime * movementDamping);
        _velocity += playerGravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);

        if (horizontalAxis != 0 && !testBox.gripActive)
        {
            this.transform.forward = Vector3.Normalize(new Vector3(horizontalAxis, 0, 0));
        }
    }

}
