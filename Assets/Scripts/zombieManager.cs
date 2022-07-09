using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieManager : MonoBehaviour
{
	public zombiePathfinder zombiePathFinderScript;
	public zombieAnimation zombieAnimationScript;

	public Transform playerPosition;
	public Transform zombiePosition;

	float distanceFromPlayer;
	float attackDistance = 2;

	public bool hasToAttack = false;

	private void Update() {
		distanceFromPlayer = Vector3.Distance(playerPosition.transform.position, zombiePosition.transform.position);

		//setting the hasToAttack and changing speed to 0 when attacking
		if (distanceFromPlayer < attackDistance) {
			hasToAttack = true;
			zombiePathFinderScript.zombieSpeed = 0;
		}

		if (hasToAttack && zombieAnimationScript.zombieAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !zombieAnimationScript.zombieAnimator.IsInTransition(0) && zombieAnimationScript.zombieAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack")) {

			zombiePathFinderScript.zombieSpeed = zombiePathFinderScript.defaultZombieSpeed;
			hasToAttack = false;
		}
	}
}
