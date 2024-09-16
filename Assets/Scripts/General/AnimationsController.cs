using System;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    private Animator animator;
    private PlayerSpriteSetSwap playerSpriteSetSwap;

    private void Awake()
    {
        animator = transform.Find("Visuals").GetComponent<Animator>();
        playerSpriteSetSwap = transform.Find("Visuals").GetComponent<PlayerSpriteSetSwap>();
    }

    private void Start()
    {
        playerSpriteSetSwap.OnMovingOnSide += PlayerSpriteSwap_OnMovingOnSide;
        playerSpriteSetSwap.OnMovingOnFront += PlayerSpriteSwap_OnMovingOnFront;
        playerSpriteSetSwap.OnMovingOnBack += PlayerSpriteSwap_OnMovingOnBack;
        playerSpriteSetSwap.OnIdle += PlayerSpriteSwap_OnIdle;

    }

    private void PlayerSpriteSwap_OnIdle(object sender, EventArgs e)
    {
        animator.SetBool("isMoving", false);
    }

    private void PlayerSpriteSwap_OnMovingOnBack(object sender, EventArgs e)
    {
        animator.SetBool("isMoving", true);
        animator.SetFloat("verticalMovement", playerSpriteSetSwap.GetRigidBodyVelocity().y);
    }

    private void PlayerSpriteSwap_OnMovingOnFront(object sender, EventArgs e)
    {
        animator.SetBool("isMoving", true);
        animator.SetFloat("verticalMovement", playerSpriteSetSwap.GetRigidBodyVelocity().y);
    }

    private void PlayerSpriteSwap_OnMovingOnSide(object sender, EventArgs e)
    {
        animator.SetBool("isMoving", true);
        animator.SetFloat("horizontalMovement", playerSpriteSetSwap.GetRigidBodyVelocity().x);
    }
}
