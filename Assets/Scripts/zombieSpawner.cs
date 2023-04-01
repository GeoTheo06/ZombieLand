using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class zombieSpawner : MonoBehaviour
{
	GameObject[] playersPosition;
	public GameObject zombieTier1;
	GameObject gameManager;
	gameManager1 gameManagerScript;
	int makeXNegative, makeZNegative;
	public int loopCounter = 0;
	int randomXSpawnDistance, randomZSpawnDistance, playerToZombieSpawnPositionsDistance, zombieCountSpawn, zombieSpawnLimit;
	public int zombiesDeadCount;

	GameObject zombie, mapBound1, mapBound2, mapBound3, mapBound4;

	private void Start()
	{
		mapBound1 = GameObject.Find("mapBound1");
		mapBound2 = GameObject.Find("mapBound2");
		mapBound3 = GameObject.Find("mapBound3");
		mapBound4 = GameObject.Find("mapBound4");
		playersPosition = GameObject.FindGameObjectsWithTag("Player");
		gameManager = GameObject.Find("gameManager");
		gameManagerScript = gameManager.GetComponent<gameManager1>();
		playerToZombieSpawnPositionsDistance = 100;
		zombieCountSpawn = 1000;
		zombieSpawnLimit = 15;
		StartCoroutine(zombiesSpawn());
	}

	int spawnXLocation;
	int spawnZLocation;
	IEnumerator zombiesSpawn()
	{
		while (loopCounter < zombieCountSpawn && gameManagerScript.zombiesALive < zombieSpawnLimit)
		{
			randomXSpawnDistance = UnityEngine.Random.Range(1, playerToZombieSpawnPositionsDistance);
			randomZSpawnDistance = playerToZombieSpawnPositionsDistance - randomXSpawnDistance;

			makeXNegative = UnityEngine.Random.Range(0, 2);
			makeZNegative = UnityEngine.Random.Range(0, 2);

			if (makeXNegative == 1)
			{
				randomXSpawnDistance = randomXSpawnDistance * -1;
			}
			if (makeZNegative == 1)
			{
				randomZSpawnDistance = randomZSpawnDistance * -1;
			}

			spawnXLocation = Mathf.RoundToInt(randomXSpawnDistance + playersPosition[0].transform.position.x);
			spawnZLocation = Mathf.RoundToInt(playersPosition[0].transform.position.z + randomZSpawnDistance);

			//checking if these numbers are off-limits
			//if (spawnXLocation <= 405 && spawnXLocation >= 20 && spawnZLocation >= 13 && spawnZLocation <= 420) {
			if (spawnXLocation <= mapBound2.transform.position.x && spawnXLocation >= mapBound1.transform.position.x && spawnZLocation >= mapBound3.transform.position.z && spawnZLocation <= mapBound4.transform.position.z)
			{
				zombie = Instantiate(zombieTier1, new Vector3(spawnXLocation, 30, spawnZLocation), Quaternion.identity);
				zombie.name = "1zombie" + loopCounter;
				loopCounter += 1;
			}

			yield return new WaitForSeconds(0.5f); //I need this because a coroutine needs a return value
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		Destroy(gameObject.GetComponent<Rigidbody>());
	}
}
