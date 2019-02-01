using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanimController : MonoBehaviour
{
    private Animator charAnimator;
    public PlayerInputHandler playerInputHandler;
    void Awake()
    {
        charAnimator = this.GetComponent<Animator>();
    }

    void Update()
    {
        SetWalkingVelocity();
        SetIsJumping();
        SetIsPushing();
        SetIsPulling();
        SetIsDead();
        SetOnTirolesa();
    }

    public void SetWalkingVelocity()
    {   
        charAnimator.SetFloat("walkingVelocity", Mathf.Abs (playerInputHandler._velocity.x));
    }

    public void SetIsJumping()
    {
        charAnimator.SetBool("isJumping", playerInputHandler.isJumping);
    }

    public void SetIsPushing()
    {
        charAnimator.SetBool("isPushing", playerInputHandler.pushGripActive);
    }

    public void SetIsPulling()
    {
        charAnimator.SetBool("isPulling", playerInputHandler.pullGripActive);
    }

    public void SetIsDead()
    {
        charAnimator.SetBool("isDead", playerInputHandler.playerIsDead);
    }
    
    public void SetOnTirolesa()
    {
        charAnimator.SetBool("onTirolesa", playerInputHandler.tirolesaActive);
    }
}
