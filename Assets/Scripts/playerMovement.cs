using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
	public CharacterController characterController;
	public float speed = 12;
	public float gravity = -9.81f;
	public float jumpHeight = 3;

	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;
	bool isGrounded;

	Vector3 velocity;
	private void Update() {
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		//falling
		if (isGrounded && velocity.y < 0) {
			velocity.y = -2;
		}

		//moving
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		Vector3 move = transform.right * x + transform.forward* z;

		characterController.Move(move * speed * Time.deltaTime);

		velocity.y += gravity * Time.deltaTime;

		characterController.Move(velocity * Time.deltaTime);

		//jumping
		if (Input.GetButtonDown("Jump") && isGrounded) {
			velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
		}
	}
}
