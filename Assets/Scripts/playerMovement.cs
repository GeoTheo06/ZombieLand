using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController characterController;
    float speed = 5;
    float maxSpeed;
    float playerGravity = -40;
    float jumpHeight = 1.5f;

    public Transform groundCheck;
    public Transform ceilingCheck;
    float groundDistance = 0.4f;
    float sprintSpeed = 5;
    public LayerMask groundMask;
    bool isGrounded;
    bool hitCeiling;
    float z;
    float x;
    public bool playerDying = false;

    bool speedBecameMaxSpeedAgain;
    public Animator playerMotion;

    public GameObject character;
    Vector3 velocity;

    private void Start()
    {
        maxSpeed = speed;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        hitCeiling = Physics.CheckSphere(ceilingCheck.position, groundDistance, groundMask);
        playerMotion.SetBool("isGrounded", isGrounded);

        //falling
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

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
            }
            else
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
            }
            else
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
            }
            else
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
            }
            else
            {
                playerMotion.SetBool("walkingStraight", false);
            }
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            z = 0;
            playerMotion.SetBool("walkingStraight", false);
        }

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

        playerMotion.SetFloat("animationSpeed", z);
        playerMotion.SetFloat("strafeWalkingSpeed", x);

        //if player presses forward and sideways button at the same time, he will gain speed because move will be equal to 2 (x + z = 1 + 1 = 2) so here: characterController.Move(move * speed * Time.deltaTime); the speed of the character will be 2 times greater than the maximum allowed speed and we don't want that so we abstract 1 in total from the speed.
        Vector3 move = transform.right * x + transform.forward * z;
        if (move.x > 1)
        {
            move.x -= 0.5f;
        }
        else if (move.x < -1)
        {
            move.x += 0.5f;
        }
        else if (move.z > 1)
        {
            move.z -= 0.5f;
        }
        else if (move.z < -1)
        {
            move.z += 0.5f;
        }

        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += playerGravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        //jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * playerGravity);
        }

        if (hitCeiling)
        {
            velocity.y = playerGravity / 10;
        }

        //spint
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed += sprintSpeed;
            maxSpeed += sprintSpeed;
            playerMotion.SetBool("isRunning", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed -= sprintSpeed;
            maxSpeed -= sprintSpeed;
            playerMotion.SetBool("isRunning", false);
        }

        //player Die
        if (playerDying)
        {
            playerMotion.SetBool("isDying", true);
        }
    }
}
