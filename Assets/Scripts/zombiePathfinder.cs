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

	public Animator enemyAnimator;

	private void FixedUpdate() {
		GetComponent<NavMeshAgent>().destination = player.transform.position;
		if (GetComponent<NavMeshAgent>().velocity.magnitude > 1) {
			enemyAnimator.SetBool("isRunning", true);
		} else {
			enemyAnimator.SetBool("isRunning", false);
		}
	}
}
