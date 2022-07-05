using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
	public CharacterController characterController;
	float speed = 5;
	float playerGravity = -40;
	float jumpHeight = 3;

	public Transform groundCheck;
	public Transform ceilingCheck;
	float groundDistance = 0.4f;
	float sprintSpeed = 5;
	public LayerMask groundMask;
	bool isGrounded;
	bool hitCeiling;
	float z;
	float x;
	
	public Animator playerMotion;

	public GameObject character;
	Vector3 velocity;

	private void Update() {
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		hitCeiling = Physics.CheckSphere(ceilingCheck.position, groundDistance, groundMask);
		playerMotion.SetBool("isGrounded", isGrounded);
		//falling
		if (isGrounded && velocity.y < 0) {
			velocity.y = -2;
		}

		//moving

		//controller
		/*float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");*/

		//player starts pressing buttons to move
		if (Input.GetKey(KeyCode.A)) {
				x = -1;
			if (isGrounded) {
				playerMotion.SetBool("strafeWalking", true);
			} else {
				playerMotion.SetBool("strafeWalking", false);
			}
		}
		if (Input.GetKey(KeyCode.D)) {
				x = 1;
			if (isGrounded) {
				playerMotion.SetBool("strafeWalking", true);
			} else {
				playerMotion.SetBool("strafeWalking", false);
			}
		}
		if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) {
			x = 0;
			playerMotion.SetBool("strafeWalking", false);
		}

		if (Input.GetKey(KeyCode.W)) {
				z = 1;
			if (isGrounded) {
				playerMotion.SetBool("walkingStraight", true);
			} else {
				playerMotion.SetBool("walkingStraight", false);
			}
		}
		if (Input.GetKey(KeyCode.S)) {
				z = -1;
			if (isGrounded) {
				playerMotion.SetBool("walkingStraight", true);
			} else {
				playerMotion.SetBool("walkingStraight", false);
			}
		}
		if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) {
			z = 0;
			playerMotion.SetBool("walkingStraight", false);
		}

		//if player stopped moving
		if (Input.GetKeyUp(KeyCode.A)) {
			x = 0;
			playerMotion.SetBool("strafeWalking", false);
		}
		if (Input.GetKeyUp(KeyCode.D)) {
			x = 0;
			playerMotion.SetBool("strafeWalking", false);
		}

		if (Input.GetKeyUp(KeyCode.W)) {
			z = 0;
			playerMotion.SetBool("walkingStraight", false);
		}
		if (Input.GetKeyUp(KeyCode.S)) {
			z = 0;
			playerMotion.SetBool("walkingStraight", false);
		}
		
		playerMotion.SetFloat("animationSpeed", z);
		playerMotion.SetFloat("strafeWalkingSpeed", x);

		Vector3 move = transform.right * x + transform.forward* z;

		characterController.Move(move * speed * Time.deltaTime);

		velocity.y += playerGravity * Time.deltaTime;

		characterController.Move(velocity * Time.deltaTime);

		//jumping
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
			velocity.y = Mathf.Sqrt(jumpHeight * -2 * playerGravity);
		}

		if (hitCeiling) {
			velocity.y = playerGravity / 10;
		}

		//spint
		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			speed += sprintSpeed;
			playerMotion.SetBool("isRunning", true);
		} else if (Input.GetKeyUp(KeyCode.LeftShift)) {
			speed -= sprintSpeed;
			playerMotion.SetBool("isRunning", false);
		}
	}
}
