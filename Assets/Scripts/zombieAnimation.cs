using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieAnimation : MonoBehaviour
{

	public Animator zombieAnimator;

	public zombiePathfinder zombiePathFinderScript;
	public zombieManager zombieManagerScript;
	private void Update() {

		if (zombiePathFinderScript.zombieSpeed > 1) {
			zombieAnimator.SetBool("isRunning", true);
			zombieAnimator.SetBool("isAttacking", false);
			zombieAnimator.SetBool("isBiting", false);
		}

		if (zombieManagerScript.hasToAttack) {
			zombieAnimator.SetBool("isRunning", false);
			zombieAnimator.SetBool("isAttacking", true);
		} else {
			zombieAnimator.SetBool("isAttacking", false);
		}
	}
}
