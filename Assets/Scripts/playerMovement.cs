using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
	public CharacterController characterController;
	float speed = 5, maxSpeed, playerGravity = -40, jumpHeight = 1.5f, groundDistance = 0.4f, sprintSpeed = 5, x, z, cameraStartingY, characterControllerStartingCenterY, characterControllerStartingHeight, cameraCrouchingY = 0.897f, characterControllerCrouchingCenterY = 0.627f, characterControllerCrouchingHeight = 1.23f;

	public Transform groundCheck, ceilingCheck;
	public LayerMask groundMask;
	bool isGrounded, hitCeiling, crouching;
	public bool playerDying = false;

	public Animator playerMotion;
	public GameObject character;
	Vector3 velocity, move;
	GameObject camera1;
	Vector3 cameraCoordinates;
	private void Start()
	{
		maxSpeed = speed;
		camera1 = GameObject.Find("camera1");
		cameraCoordinates = camera1.transform.localPosition;

		//getting the default values (as the game starts)
		cameraStartingY = cameraCoordinates.y;
		characterControllerStartingCenterY = characterController.center.y;
		characterControllerStartingHeight = characterController.height;
	}

	private void Update()
	{
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		hitCeiling = Physics.CheckSphere(ceilingCheck.position, groundDistance, groundMask);
		playerMotion.SetBool("isGrounded", isGrounded);

		playerFalling();
		playerMoving();
		playerStopsMoving();

		playerMotion.SetFloat("animationSpeed", z);
		playerMotion.SetFloat("strafeWalkingSpeed", x);

		move = transform.right * x + transform.forward * z;

		fixStrafingUnexpectedSpeed();
		actuallyMoveCharacter();
		playerJumping();
		playerSprinting();
		playerDies();


		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			crouching = true;
			playerCrouching();
		} else if (Input.GetKeyUp(KeyCode.LeftControl))
		{
			crouching = false;
			playerCrouching();
		}
	}

	void playerFalling()
	{
		//falling
		if (isGrounded && velocity.y < 0)
		{
			velocity.y = -2;
		}
	}

	void playerMoving()
	{
		//moving with controller
		/*float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");*/

		//player starts pressing buttons to move
		if (Input.GetKey(KeyCode.A))
		{
			x = -1;
			if (isGrounded)
			{
				playerMotion.SetBool("strafeWalking", true);
			} else
			{
				playerMotion.SetBool("strafeWalking", false);
			}
		}
		if (Input.GetKey(KeyCode.D))
		{
			x = 1;
			if (isGrounded)
			{
				playerMotion.SetBool("strafeWalking", true);
			} else
			{
				playerMotion.SetBool("strafeWalking", false);
			}
		}
		if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
		{
			x = 0;
			playerMotion.SetBool("strafeWalking", false);
		}

		if (Input.GetKey(KeyCode.W))
		{
			z = 1;
			if (isGrounded)
			{
				playerMotion.SetBool("walkingStraight", true);
			} else
			{
				playerMotion.SetBool("walkingStraight", false);
			}
		}
		if (Input.GetKey(KeyCode.S))
		{
			z = -1;
			if (isGrounded)
			{
				playerMotion.SetBool("walkingStraight", true);
			} else
			{
				playerMotion.SetBool("walkingStraight", false);
			}
		}
		if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
		{
			z = 0;
			playerMotion.SetBool("walkingStraight", false);
		}
	}

	void playerStopsMoving()
	{
		//if player stops moving
		if (Input.GetKeyUp(KeyCode.A))
		{
			x = 0;
			playerMotion.SetBool("strafeWalking", false);
		}
		if (Input.GetKeyUp(KeyCode.D))
		{
			x = 0;
			playerMotion.SetBool("strafeWalking", false);
		}

		if (Input.GetKeyUp(KeyCode.W))
		{
			z = 0;
			playerMotion.SetBool("walkingStraight", false);
		}
		if (Input.GetKeyUp(KeyCode.S))
		{
			z = 0;
			playerMotion.SetBool("walkingStraight", false);
		}
	}

	void fixStrafingUnexpectedSpeed()
	{
		//if player presses forward and sideways button at the same time, he will gain speed because move will be equal to 2 (x + z = 1 + 1 = 2) so here: characterController.Move(move * speed * Time.deltaTime); the speed of the character will be 2 times greater than the maximum allowed speed and we don't want that so we abstract 1 in total from the speed.

		if (move.x > 1)
		{
			move.x -= 0.5f;
		} else if (move.x < -1)
		{
			move.x += 0.5f;
		} else if (move.z > 1)
		{
			move.z -= 0.5f;
		} else if (move.z < -1)
		{
			move.z += 0.5f;
		}
	}

	void playerJumping()
	{
		//jumping
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			velocity.y = Mathf.Sqrt(jumpHeight * -2 * playerGravity);
		}

		if (hitCeiling)
		{
			velocity.y = playerGravity / 10;
		}
	}

	void playerSprinting()
	{
		//spint
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			speed += sprintSpeed;
			maxSpeed += sprintSpeed;
			playerMotion.SetBool("isRunning", true);
		} else if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			speed -= sprintSpeed;
			maxSpeed -= sprintSpeed;
			playerMotion.SetBool("isRunning", false);
		}
	}

	void actuallyMoveCharacter()
	{
		characterController.Move(move * speed * Time.deltaTime);

		velocity.y += playerGravity * Time.deltaTime;

		characterController.Move(velocity * Time.deltaTime);
	}

	void playerDies()
	{
		//player Die
		if (playerDying)
		{
			playerMotion.SetBool("isDying", true);
		}
	}

	void playerCrouching()
	{

		if (crouching)
		{
			//set character controller center
			characterController.center = new Vector3(0, characterControllerCrouchingCenterY, 0);
			characterController.height = characterControllerCrouchingHeight;

			//set camera position

			camera1.transform.localPosition = new Vector3(cameraCoordinates.x, cameraCrouchingY, cameraCoordinates.z);

		} else
		{
			//set character controller center
			characterController.center = new Vector3(0, characterControllerStartingCenterY, 0);
			characterController.height = characterControllerStartingHeight;

			//set camera position
			camera1.transform.localPosition = new Vector3(cameraCoordinates.x, cameraStartingY, cameraCoordinates.z);
		}
	}
}