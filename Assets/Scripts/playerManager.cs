using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
	GameObject player;
	GameObject gameManager;
	GameObject camera1;
	gameManager1 gameManagerScript;
	playerMovement playerMovementScript;
	cameraMovement cameraMovementScript;
	public int playerHealth;
	public bool playerDead = false;

	public Animator cameraDie;

	private void Start()
	{
		player = GameObject.Find("player1");
		gameManager = GameObject.Find("gameManager");
		camera1 = GameObject.Find("camera1");
		playerHealth = 1000;
		playerMovementScript = player.GetComponent<playerMovement>();
		gameManagerScript = gameManager.GetComponent<gameManager1>();
		cameraMovementScript = camera1.GetComponent<cameraMovement>();
	}

	private void Update()
	{
		if (playerHealth <= 0)
		{
			playerMovementScript.playerDying = true; // starting animation for dying
			cameraMovementScript.enabled = false; //disabling camera control
			gameManagerScript.gameOver = true; //telling the game manager that the game has finished
			cameraDie.SetBool("isDying", true);
			playerDead = true;
		}
	}
}
