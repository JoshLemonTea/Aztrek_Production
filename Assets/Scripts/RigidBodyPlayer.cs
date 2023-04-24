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

    private void FixedUpdate()
	{
		grounded = false;
		List<Collider> hits = new List<Collider>();
		hits.AddRange(Physics.OverlapSphere(transform.position - new Vector3(0, 0.5f, 0), 1.1f));
		foreach(Collider hit in hits)
        {
			if (hit.tag != "Player")
            {
				grounded = true;
            }
        }
		

		Move();
		FaceForward();
	}

	public void Move()
	{
		Vector3 relativeMovement = RelativeMovementVector();
		//only control the player if grounded or airControl is turned on
		if (grounded)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector3(relativeMovement.x * speed, rBody.velocity.y, relativeMovement.y * speed);
			// And then smoothing it out and applying it to the character
			rBody.velocity = Vector3.SmoothDamp(rBody.velocity, targetVelocity, ref velocity, movementSmoothing);
		}
		if (airControl)
		{
			if (movementInput.magnitude > 0.1f)
			{
				// Move the character by finding the target velocity
				Vector3 targetVelocity = new Vector3(relativeMovement.x * speed, rBody.velocity.y, relativeMovement.y * speed);
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
            Vector3 normalisedVelocity = new Vector3(rBody.velocity.x, 0, rBody.velocity.z).normalized;
            transform.forward = normalisedVelocity;
        }
    }

	private Vector3 RelativeMovementVector()
	{
		float horizontalAxis = movementInput.x;
		float verticalAxis = movementInput.y;

		//camera forward and right vectors:
		var forward = Camera.main.transform.forward;
		var right = Camera.main.transform.right;

		//project forward and right vectors on the horizontal plane (y = 0)
		forward.y = 0f;
		right.y = 0f;
		forward.Normalize();
		right.Normalize();

		//this is the direction in the world space we want to move:
		var desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;
		desiredMoveDirection.y = desiredMoveDirection.z;
		desiredMoveDirection.z = 0;
		return desiredMoveDirection;
	}
}
