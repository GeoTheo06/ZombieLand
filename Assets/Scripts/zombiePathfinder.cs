using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombiePathfinder : MonoBehaviour
{
	public NavMeshAgent zombiePathFinder;
	public GameObject player;

	public float zombieSpeed;
	public float defaultZombieSpeed = 9;
	private void Start() {
		zombieSpeed = defaultZombieSpeed;
	}

	

	private void FixedUpdate() {
		zombiePathFinder.destination = player.transform.position;

		zombiePathFinder.speed = zombieSpeed;
	}
}
