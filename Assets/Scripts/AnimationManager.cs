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
    [SerializeField] private Player playerScript;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = new Vector2(playerScript.Movement.x, playerScript.Movement.z).magnitude;
        velocityY = playerScript.Movement.y + 1;
        isGrounded = playerScript.IsGrounded;
        isHovering = playerScript.IsHovering;
        isSwinging = playerScript.IsGrappling;

        if (isGrounded)
        {
            animator.ResetTrigger("Jumped");
        }

        SetParams();
    }

    public void OnJump()
    {
        if (isGrounded)
        {
            animator.ResetTrigger("Jumped");
            animator.SetTrigger("Jumped");
        }
    }

    private void SetParams()
    {
        animator.SetFloat("Speed", speed);
        animator.SetFloat("VelocityY", velocityY);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsHovering", isHovering);
        animator.SetBool("IsSwinging", isSwinging);
    }
}
