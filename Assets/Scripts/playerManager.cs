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
	public bool playerDead = false;
	GameObject groundCheck, ceilingCheck;
	CharacterController characterController;

	public Animator cameraDie;
	Vector3 bottomOfCharacterController, topOfCharacterController;

	private void Start()
	{
		player = GameObject.Find("player1");
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
		bottomOfCharacterController = new Vector3(characterController.transform.position.x, 0, characterController.transform.position.z);
		topOfCharacterController = new Vector3(characterController.transform.position.x, characterController.transform.position.y + characterController.height / 2, characterController.transform.position.z); ;
		groundCheck.transform.position = bottomOfCharacterController;
		ceilingCheck.transform.position = topOfCharacterController;
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
