using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombiePathfinder : MonoBehaviour
{
	GameObject player;

	public float zombieSpeed;
	public float defaultZombieSpeed = 9;
	bool startRunningToPlayer;

	private void Start() {
		player = GameObject.Find("player1");
		toggleNavMeshAgent(0);
		zombieSpeed = defaultZombieSpeed;
		startRunningToPlayer = false;
	}

	private void FixedUpdate() {
		if (startRunningToPlayer) {
			if (gameObject.GetComponent<NavMeshAgent>().isActiveAndEnabled) {
				gameObject.GetComponent<NavMeshAgent>().destination = player.transform.position;
			}
		}

		gameObject.GetComponent<NavMeshAgent>().speed = zombieSpeed;
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag != "zombieTier1") {
			toggleNavMeshAgent(1);
			startRunningToPlayer = true;
		}
	}

	public void toggleNavMeshAgent(int enableOrDisable) {
		if (enableOrDisable == 1) {
			gameObject.GetComponent<NavMeshAgent>().enabled = true;
		} else if (enableOrDisable == 0) {
		gameObject.GetComponent<NavMeshAgent>().enabled = false;
		}
	}
} 