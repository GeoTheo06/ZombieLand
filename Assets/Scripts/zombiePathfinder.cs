using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombiePathfinder : MonoBehaviour
{
	GameObject player, playerManager;
	playerManager playerManagerScript;

	public float defaultZombieSpeed = 9;
	float maxSpeed = 15f;
	float speedIncreaseRate = 0.1f;
	bool startRunningToPlayer;

	private void Start()
	{
		player = GameObject.Find("player1");
		playerManager = GameObject.Find("playerManager");
		toggleNavMeshAgent(0);
		startRunningToPlayer = false;
		playerManagerScript = playerManager.GetComponent<playerManager>();
		gameObject.GetComponent<NavMeshAgent>().speed = defaultZombieSpeed;
	}

	private void Update()
	{
		if (startRunningToPlayer)
		{
			if (gameObject.GetComponent<NavMeshAgent>().isActiveAndEnabled && !playerManagerScript.playerDead)
			{
				gameObject.GetComponent<NavMeshAgent>().destination = player.transform.position;
			}
			else
			{
				gameObject.GetComponent<NavMeshAgent>().enabled = false;
			}
		}
		if (!gameObject.GetComponent<zombieManager>().hasToAttack)
		{
			gameObject.GetComponent<NavMeshAgent>().speed += speedIncreaseRate * Time.deltaTime;
			gameObject.GetComponent<NavMeshAgent>().speed = Mathf.Clamp(gameObject.GetComponent<NavMeshAgent>().speed, defaultZombieSpeed, maxSpeed);
		}
	}


	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag != "zombieTier1")
		{
			toggleNavMeshAgent(1);
			startRunningToPlayer = true;
		}
	}

	public void toggleNavMeshAgent(int enableOrDisable)
	{
		if (enableOrDisable == 1)
		{
			gameObject.GetComponent<NavMeshAgent>().enabled = true;
		}
		else if (enableOrDisable == 0)
		{
			gameObject.GetComponent<NavMeshAgent>().enabled = false;
		}
	}
}
