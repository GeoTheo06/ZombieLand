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

	private void Start() {
		player = GameObject.Find("player1");
		gameObject.GetComponent<NavMeshAgent>().enabled = false;
		zombieSpeed = defaultZombieSpeed;
	}

	private void FixedUpdate() {
		zombiePathFinder.destination = player.transform.position;

		zombiePathFinder.speed = zombieSpeed;
	}

	private void OnCollisionEnter(Collision collision) {
		Destroy(gameObject.GetComponent<Rigidbody>());
		gameObject.GetComponent<NavMeshAgent>().enabled = true;
	}
}
