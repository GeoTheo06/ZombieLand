using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieAnimation : MonoBehaviour
{
	public Animator zombieAnimator;
	GameObject gameManager;
	gameManager1 gameManagerScript;

	public zombiePathfinder zombiePathFinderScript;
	public zombieManager zombieManagerScript;

	private void Start()
	{
		gameManager = GameObject.Find("gameManager");
		gameManagerScript = gameManager.GetComponent<gameManager1>();
	}

	private void Update()
	{
		zombieRunning();
		zombieAttacking();
		zombieDying();
		zombieIdle();

	}

	void zombieRunning()
	{
		if (zombiePathFinderScript.zombieSpeed > 1)
		{
			zombieAnimator.SetBool("isRunning", true);
			zombieAnimator.SetBool("isAttacking", false);
			zombieAnimator.SetBool("isBiting", false);
		}
	}

	void zombieAttacking()
	{
		if (zombieManagerScript.hasToAttack)
		{
			zombieAnimator.SetBool("isRunning", false);
			zombieAnimator.SetBool("isAttacking", true);
		} else
		{
			zombieAnimator.SetBool("isAttacking", false);
			zombieAnimator.SetBool("isRunning", true);
		}
	}

	void zombieDying()
	{
		if (zombieManagerScript.isDying)
		{
			zombieAnimator.SetBool("isDying", true);
		}
	}

	void zombieIdle()
	{
		if (gameManagerScript.gameOver)
		{
			zombieAnimator.SetBool("isRunning", false);
			zombieAnimator.SetBool("isAttacking", false);
			zombieAnimator.SetBool("isBiting", false);
			zombieAnimator.SetBool("isScreaming", false);
		}
	}
}
