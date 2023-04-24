using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RigidBodyPlayer : MonoBehaviour
{
	[SerializeField] private float speed = 7;
	[SerializeField] private float jumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool airControl = false;                         // Whether or not a player can steer while jumping;

	private Vector3 movementInput;
	private bool jumped;
	private bool grounded;
	private Rigidbody rBody;
	private Vector3 velocity = Vector3.zero;

	public void OnMove(InputAction.CallbackContext context)
	{
		movementInput = context.ReadValue<Vector2>();
	}

	public void OnJump(InputAction.CallbackContext context)
	{
		if (context.ReadValue<float>() == 0)
		{
			jumped = false;
		}
		else
		{
			jumped = true;
		}
		jumped = context.action.triggered;
	}


	private void Awake()
	{
		rBody = GetComponent<Rigidbody>();
	}

    private void OnDrawGizmos()
    {
		Vector3 sphereCenter = transform.position - new Vector3(0, (GetComponent<CapsuleCollider>().height / 2));
		Gizmos.DrawSphere(sphereCenter, 1);
    }

    private void FixedUpdate()
	{
		grounded = false;
		RaycastHit hit;
		if (Physics.SphereCast(transform.position, 1, -Vector3.up, out hit, 1.5f))
		{
			grounded = true;
		}

		Move();
		Debug.Log(grounded);
		FaceForward();
	}

	public void Move()
	{
		//only control the player if grounded or airControl is turned on
		if (grounded)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector3(movementInput.x * speed, rBody.velocity.y, movementInput.y * speed);
			// And then smoothing it out and applying it to the character
			rBody.velocity = Vector3.SmoothDamp(rBody.velocity, targetVelocity, ref velocity, movementSmoothing);
		}
		if (airControl)
		{
			if (movementInput.magnitude > 0.1f)
			{
				// Move the character by finding the target velocity
				Vector3 targetVelocity = new Vector3(movementInput.x * speed, rBody.velocity.y, movementInput.y * speed);
				// And then smoothing it out and applying it to the character
				rBody.velocity = Vector3.SmoothDamp(rBody.velocity, targetVelocity, ref velocity, 0.5f);
			}
		}
		// If the player should jump...
		if (grounded && jumped)
		{
			Jump();
		}
	}

	private void Jump()
	{
		// Add a vertical force to the player.
		grounded = false;
		jumped = false;
		rBody.velocity = new Vector3(rBody.velocity.x, jumpForce, rBody.velocity.z);

	}

	private void FaceForward()
    {
        if (movementInput != Vector3.zero)
        {
            //Vector3 normalisedVelocity = new Vector3(rBody.velocity.x, 0, rBody.velocity.z).normalized;
            //transform.forward = normalisedVelocity;
        }
    }
}
