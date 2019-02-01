#define DEBUGMODE
using System.Collections;

using UnityEngine;
using UnityEngine.Audio;

public class PlayerInputHandler : MonoBehaviour
{
    public float playerSpeed = 20f;
    public float jumpHeight = 3f;
    public float groundDamping = 10f;
    public float airDamping = 5f;

    public bool pushGripActive { get; set; }
    public bool pullGripActive { get; set; }
    public bool handleActive { get; set; }
    public bool isJumping;
    public bool canMove;
    public bool playerIsDead { get; set; }
    public bool tirolesaActive;

    public Vector3 playerGravity;
    public Vector3 _velocity;
    private Vector3 moveDirection;

    private Collider[] hitColliders;

    [SerializeField] private AudioSource dragBoxSFX;

    private PrototypeCharacterControllerv2 _controller;

#if DEBUGMODE
    private Renderer _renderer;
#endif

    #region MonoBehaviour
    private void Awake()
    {
        tirolesaActive = false;
        _controller = GetComponent<PrototypeCharacterControllerv2>();

#if DEBUGMODE
        _renderer = GetComponent<Renderer>();
#endif
    }

    private void Update()
    {
        //Controle de velocidade do personagem para storyteeling
        if (this.transform.position.x < 10.5f || (this.transform.position.x > 296f && this.transform.position.x < 310f))
            playerSpeed = 3f;
        else if ((this.transform.position.x > 109f && this.transform.position.x < 121f) || this.transform.position.x > 546f)
            playerSpeed = 6f;
        else
            playerSpeed = 8f;

        if (_controller.isGrounded)
            isJumping = false;
        //********************

        Interact();

        _velocity = _controller.velocity;
        Time.timeScale = Input.GetKey(KeyCode.Space) ? 0.1f : 1f;

#if DEBUGMODE
        _renderer.material.color = _controller.isGrounded ? Color.green : Color.red;
#endif

        float horizontalAxis = Input.GetAxis("Horizontal");

        bool jumpKeys = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        if (jumpKeys && _controller.isGrounded && !pushGripActive || jumpKeys && _controller.isGrounded && !handleActive)
        {
            if (!pushGripActive && !pullGripActive)
            {
                isJumping = true;
                _velocity.y = Mathf.Sqrt(2f * jumpHeight * -playerGravity.y);
            }
        }

        float movementDamping = _controller.isGrounded ? groundDamping : airDamping;

        _velocity.x = Mathf.Lerp(_velocity.x, horizontalAxis * playerSpeed, Time.deltaTime * movementDamping/3f); //MUDANÇA AQUI!!! DIVIDI POR 3!!! ASS.: RODRIGO VIEIRA
        _velocity += playerGravity * Time.deltaTime;

        if (canMove)
            _controller.Move(_velocity * Time.deltaTime);

        if (horizontalAxis != 0 && !pushGripActive && !pullGripActive)
            this.transform.forward = Vector3.Normalize(new Vector3(horizontalAxis, 0, 0));
    }
    #endregion

    private void Interact()
    {
        if (Input.GetKey(KeyCode.K))
        {
            float moveDir = Input.GetAxis("Horizontal");
            hitColliders = Physics.OverlapBox(this.transform.position + transform.forward, Vector3.one * 0.2f);
            foreach (Collider col in hitColliders)
            {
                SetDistanceBetweenPlayerandBox(this.gameObject, col.gameObject);

                if (col.gameObject.CompareTag("PushBox") || col.gameObject.CompareTag("BigPushBox")) // caixa de Empurrar
                {
                    Debug.Log("CAIXA DE EMPURRAR");
                    pushGripActive = true;
                    if (moveDir > 0f && col.gameObject.transform.position.x > this.transform.position.x ||
                        moveDir < 0f && col.gameObject.transform.position.x < this.transform.position.x)
                    {
                        playerSpeed = 3f;
                        col.gameObject.transform.SetParent(this.transform);
                        col.gameObject.layer = 9; // Layer MoveableObject
                        this.AudioDragBox(true);
                    }
                    else
                    {
                        pushGripActive = false;
                        col.gameObject.transform.SetParent(null);
                        col.gameObject.layer = 0;
                        this.AudioDragBox(false);
                    }
                }
                if (col.gameObject.CompareTag("PullBox")) // caixa de puxar
                {
                    Debug.Log("CAIXA DE PUXAR");
                    pullGripActive = true;
                    if (moveDir < 0f && col.gameObject.transform.position.x > this.transform.position.x ||
                        moveDir > 0f && col.gameObject.transform.position.x < this.transform.position.x)
                    {
                        playerSpeed = 3f;
                        col.gameObject.transform.SetParent(this.transform);
                        col.gameObject.layer = 9;
                        this.AudioDragBox(true);
                    }
                    else
                    {
                        pullGripActive = false;
                        col.gameObject.transform.SetParent(null);
                        col.gameObject.layer = 0;
                        this.AudioDragBox(false);
                    }
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            playerSpeed = 8f;
            foreach (Collider col in hitColliders)
            {
                if (col.gameObject.CompareTag("PushBox") || col.gameObject.CompareTag("BigPushBox")) // caixa de empurrar
                {
                    pushGripActive = false;
                    col.gameObject.transform.SetParent(null);
                    col.gameObject.layer = 0;
                    this.AudioDragBox(false);
                }
                if (col.gameObject.CompareTag("PullBox")) // caixa de puxar
                {
                    pullGripActive = false;
                    col.gameObject.transform.SetParent(null);
                    col.gameObject.layer = 0;
                    this.AudioDragBox(false);
                }
            }
        }
    }

    public void SetDistanceBetweenPlayerandBox(GameObject player, GameObject box)
    {
        string typeOfBox = box.tag;

        switch(typeOfBox)
        {
            case "PushBox":
                if (player.transform.position.x < box.transform.position.x)
                    player.transform.SetPositionAndRotation(new Vector3(box.transform.position.x - 2.2f,player.transform.position.y, player.transform.position.z), player.gameObject.transform.rotation);
                else
                    player.transform.SetPositionAndRotation(new Vector3(box.transform.position.x + 2.2f, player.transform.position.y, player.transform.position.z), player.gameObject.transform.rotation);
                return;

            case "PullBox":
                if (player.transform.position.x < box.transform.position.x)
                    player.transform.SetPositionAndRotation(new Vector3(box.transform.position.x - 2.2f, player.transform.position.y, player.transform.position.z), player.gameObject.transform.rotation);
                else
                    player.transform.SetPositionAndRotation(new Vector3(box.transform.position.x + 2.2f, player.transform.position.y, player.transform.position.z), player.gameObject.transform.rotation);
                return;

            case "BigPushBox":
                if (player.transform.position.x < box.transform.position.x)
                    player.transform.SetPositionAndRotation(new Vector3(box.transform.position.x - 5.8f, player.transform.position.y, player.transform.position.z), player.gameObject.transform.rotation);
                else
                    player.transform.SetPositionAndRotation(new Vector3(box.transform.position.x + 5.8f, player.transform.position.y, player.transform.position.z), player.gameObject.transform.rotation);
                return;
            default:
                return;
        }
    }

    public void KillPlayer()
    {
        playerIsDead = true;
    }

    public void AudioDragBox(bool active)
    {
        if (active)
        {
            if (!dragBoxSFX.isPlaying)
                dragBoxSFX.PlayDelayed(0.15f);
        }
        else
            dragBoxSFX.Stop();
    }
}