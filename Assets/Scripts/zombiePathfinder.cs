using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombiePathfinder : MonoBehaviour
{
	public NavMeshAgent zombiePathFinder;
	GameObject player;

	public float zombieSpeed;
	public float defaultZombieSpeed;
	bool startRunningToPlayer;

	private void Start() {
		player = GameObject.Find("player1");
		gameObject.GetComponent<NavMeshAgent>().enabled = false;
		zombieSpeed = defaultZombieSpeed;
		startRunningToPlayer = false;
<<<<<<< HEAD
=======
		playerManagerScript = playerManager.GetComponent<playerManager>();
		defaultZombieSpeed = 10;
>>>>>>> parent of 24573ee (Revert "changed some values for better gameplay")
	}

	private void FixedUpdate() {
		if (startRunningToPlayer) {
			zombiePathFinder.destination = player.transform.position;
		}

		zombiePathFinder.speed = zombieSpeed;
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag != "zombieTier1") {
			gameObject.GetComponent<NavMeshAgent>().enabled = true;
			startRunningToPlayer = true;
		}
	}
}