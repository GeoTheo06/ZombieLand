using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombiePathfinder : MonoBehaviour
{
	public NavMeshAgent zombiePathFinder;
	GameObject player;

	public float zombieSpeed;
	public float defaultZombieSpeed = 9;
	bool startRunningToPlayer;

	private void Start() {
		player = GameObject.Find("player1");
		gameObject.GetComponent<NavMeshAgent>().enabled = false;
		zombieSpeed = defaultZombieSpeed;
		startRunningToPlayer = false;
	}

	private void FixedUpdate() {
		if (startRunningToPlayer) {
			zombiePathFinder.destination = player.transform.position;
		}

		zombiePathFinder.speed = zombieSpeed;
	}

	private void OnCollisionEnter(Collision collision) {
		gameObject.GetComponent<NavMeshAgent>().enabled = true;
		startRunningToPlayer = true;

	}
}