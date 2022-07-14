using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class zombieManager : MonoBehaviour
{
	GameObject player;
	GameObject playerManager;
	GameObject zombieChild;
	public zombiePathfinder zombiePathFinderScript;
	public zombieAnimation zombieAnimationScript;
	playerManager playerManagerScript;
	Transform playerPosition;

	float distanceFromPlayer;
	float attackDistance;
	int zombieAttackDamage;

	public bool hasToAttack = false;

	int zombieHealth;

	private void Start() {

		zombieChild = transform.GetChild(1).gameObject;
		hideFromCamera();

		player = GameObject.Find("player1");
		playerManager = GameObject.Find("playerManager");

		playerManagerScript = playerManager.GetComponent<playerManager>();
		playerPosition = player.GetComponent<Transform>();
		zombieHealth = 100;
		attackDistance = 2;
	}

	private void Update() {
		distanceFromPlayer = Vector3.Distance(playerPosition.transform.position, transform.position);

		//setting the hasToAttack and changing speed to 0 when attacking
		if (distanceFromPlayer < attackDistance) {
			hasToAttack = true;
			this.zombiePathFinderScript.zombieSpeed = 0;
		}


		if (hasToAttack && zombieAnimationScript.zombieAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !zombieAnimationScript.zombieAnimator.IsInTransition(0) && zombieAnimationScript.zombieAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack")) { //if animation "attack" has finished.

			this.zombiePathFinderScript.zombieSpeed = this.zombiePathFinderScript.defaultZombieSpeed;
			hasToAttack = false;
		}
	}

	public void finishedZombieAttack() { //finished attacking (the receiver is the event on the animation)
		//if the player is still in the zombie attack range, he will lose some hp
		if (distanceFromPlayer < attackDistance * 2) {
			playerManagerScript.playerHealth -= zombieAttackDamage;
			Debug.Log("you lost 100 hp");
		}
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag != "zombieTier1") {
			ShowOnCamera();
		}
	}

	int hideZombieLayer;
	public void hideFromCamera() {
		hideZombieLayer = LayerMask.NameToLayer("hideZombieTier1");

		zombieChild.layer = hideZombieLayer;
	}

	public void ShowOnCamera() {
		zombieChild.layer = 0;
	}
}
