using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private float speed;
    private float velocityY;
    private bool jumped;
    private bool isGrounded;
    private bool isHovering;
    private bool isSwinging;
    private Animator animator;

    // Update is called once per frame
    void Update()
    {
        Player playerScript = GetComponent<Player>();

        speed = new Vector2(playerScript._movement.x, playerScript._movement.z).magnitude;
        velocityY = playerScript._movement.y;
        isGrounded = playerScript.IsGrounded;
        isHovering = playerScript.IsHovering;
        isSwinging = playerScript.IsGrappling;

        if (isGrounded || velocityY < 0)
        {
            jumped = false;
        }
    }

    public void OnJump()
    {
        jumped = true;
    }

    private void SetParams()
    {
        animator.SetFloat("Speed", speed);
        animator.SetFloat("VelocityY", velocityY);
        animator.SetBool("Jumped", jumped);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsHovering", isHovering);
        animator.SetBool("isSwinging", isSwinging);
    }
}
