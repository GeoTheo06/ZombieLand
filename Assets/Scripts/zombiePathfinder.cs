using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombiePathfinder : MonoBehaviour
{
	public GameObject player;
	private void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	private void FixedUpdate() {
		GetComponent<NavMeshAgent>().destination = player.transform.position;
	}
}
