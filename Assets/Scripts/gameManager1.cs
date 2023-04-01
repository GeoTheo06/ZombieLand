using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager1 : MonoBehaviour
{
	public bool gameOver;

	GameObject[] zombies;

	GameObject zombieSpawner;
	zombieSpawner zombieSpawnerScript;

	int deadZombies;
	public int zombiesALive;
	HashSet<string> countedZombies;


	private void Start()
	{
		zombieSpawner = GameObject.Find("zombieSpawner");
		zombieSpawnerScript = zombieSpawner.GetComponent<zombieSpawner>();
		countedZombies = new HashSet<string>();
	}

	private void Update()
	{
		foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("zombieTier1"))
		{
			if (!zombie.GetComponent<zombieManager>().isDying && !countedZombies.Contains(zombie.name))
			{
				deadZombies++;
				countedZombies.Add(zombie.name);
			}
		}
		zombiesALive = zombieSpawnerScript.loopCounter - deadZombies;
		Debug.Log(zombiesALive);
		if (gameOver)
			Debug.Log("Game Over");
	}
}
