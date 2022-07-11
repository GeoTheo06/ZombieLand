using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class zombieManager : MonoBehaviour
{
	public zombiePathfinder zombiePathFinderScript;
	public zombieAnimation zombieAnimationScript;
	public playerManager playerManagerScript;

	public Transform playerPosition;
	public Transform zombiePosition;

	public GameObject zombieTier1;

	float distanceFromPlayer;
	float attackDistance;
	int zombieAttackDamage;
	
	int randomXSpawnDistance;
	int randomYSpawnDistance;
	int playerAndZombieSpawnPositionsDistance;
	int zombieCountSpawn;

	public bool hasToAttack = false;

	int zombieHealth;

	private void Start() {
		zombieHealth = 100;
		attackDistance = 2;
		zombieAttackDamage = 100;
		playerAndZombieSpawnPositionsDistance = 100;
		zombieCountSpawn = 500;
		StartCoroutine(zombiesSpawn());
	}

	private void Update() {
		distanceFromPlayer = Vector3.Distance(playerPosition.transform.position, zombiePosition.transform.position);

		//setting the hasToAttack and changing speed to 0 when attacking
		if (distanceFromPlayer < attackDistance) {
			hasToAttack = true;
			zombiePathFinderScript.zombieSpeed = 0;
		}


		if (hasToAttack && zombieAnimationScript.zombieAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !zombieAnimationScript.zombieAnimator.IsInTransition(0) && zombieAnimationScript.zombieAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack")) { //if animation "attack" has finished.

			zombiePathFinderScript.zombieSpeed = zombiePathFinderScript.defaultZombieSpeed;
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
	int loopCounter = 0;
	int makeXNegative;
	int makeZNegative;
	IEnumerator zombiesSpawn() {
		while (loopCounter < zombieCountSpawn) {
			randomXSpawnDistance = UnityEngine.Random.Range(1, playerAndZombieSpawnPositionsDistance);
			randomYSpawnDistance = playerAndZombieSpawnPositionsDistance - randomXSpawnDistance;

			makeXNegative = UnityEngine.Random.Range(0, 2);
			makeZNegative = UnityEngine.Random.Range(0, 2);

			if (makeXNegative == 1) {
				randomXSpawnDistance = randomXSpawnDistance * -1;
			}
			if (makeZNegative == 1) {
				randomYSpawnDistance = randomYSpawnDistance * -1;
			}

			Instantiate(zombieTier1, new Vector3(playerPosition.transform.position.x + randomXSpawnDistance, 0, playerPosition.transform.position.z + randomYSpawnDistance), Quaternion.identity);
			yield return new WaitForSeconds(0.5f); //I need this because a coroutine needs a return value
			loopCounter += 1;
		}
		
	}
}
