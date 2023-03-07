using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class playerManager : MonoBehaviour
{
	GameObject player, gameManager, camera1;

	gameManager1 gameManagerScript;
	playerMovement playerMovementScript;
	cameraMovement cameraMovementScript;
	public int playerHealth;
	public bool playerDead;
	GameObject groundCheck, ceilingCheck;
	CharacterController characterController;

	public Animator cameraDie;
	Vector3 bottomOfCharacterController, topOfCharacterController;
	//just testing local version controll
	private void Start()
	{
		player = GameObject.Find("player1");
		gameManager = GameObject.Find("gameManager");
		camera1 = GameObject.Find("camera1");
		playerHealth = 1000;
		playerDead = false;
		playerMovementScript = player.GetComponent<playerMovement>();
		gameManagerScript = gameManager.GetComponent<gameManager1>();
		cameraMovementScript = camera1.GetComponent<cameraMovement>();
		groundCheck = GameObject.Find("groundCheck");
		ceilingCheck = GameObject.Find("ceilingCheck");
		characterController = player.GetComponent<CharacterController>();
	}

	private void Update()
	{

		set_groundCheck_ceilingCheck_position();

		if (playerHealth <= 0)
		{
			playerDies();
		}
	}

	void set_groundCheck_ceilingCheck_position()
	{
		//set groundCheck, ceilingCheck position

		groundCheck.transform.position = new Vector3(characterController.transform.position.x, player.transform.position.y, characterController.transform.position.z);

		ceilingCheck.transform.localPosition = new Vector3(0, characterController.height, 0);

	}
	void playerDies()
	{
		playerMovementScript.playerDying = true; // starting animation for dying
		cameraMovementScript.enabled = false; //disabling camera control
		gameManagerScript.gameOver = true; //telling the game manager that the game has finished
		cameraDie.SetBool("isDying", true);
		playerDead = true;
	}
}
