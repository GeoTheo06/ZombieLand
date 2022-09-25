using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class zombieSpawner : MonoBehaviour {
	GameObject[] playersPosition;
	public GameObject zombieTier1;
	int loopCounter = 0;
	int makeXNegative;
	int makeZNegative;

	int randomXSpawnDistance;
	int randomZSpawnDistance;
	int playerToZombieSpawnPositionsDistance;
	int zombieCountSpawn;


	GameObject zombie;

	private void Start() {

		playersPosition = GameObject.FindGameObjectsWithTag("Player");
		playerToZombieSpawnPositionsDistance = 100;
		zombieCountSpawn = 0;
		StartCoroutine(zombiesSpawn());
	}
	int spawnXLocation;
	int spawnZLocation;
	IEnumerator zombiesSpawn() {
		while (loopCounter < zombieCountSpawn) {
			randomXSpawnDistance = UnityEngine.Random.Range(1, playerToZombieSpawnPositionsDistance);
			randomZSpawnDistance = playerToZombieSpawnPositionsDistance - randomXSpawnDistance;

			makeXNegative = UnityEngine.Random.Range(0, 2);
			makeZNegative = UnityEngine.Random.Range(0, 2);

			if (makeXNegative == 1) {
				randomXSpawnDistance = randomXSpawnDistance * -1;
			}
			if (makeZNegative == 1) {
				randomZSpawnDistance = randomZSpawnDistance * -1;
			}
			spawnXLocation = Mathf.RoundToInt(randomXSpawnDistance + playersPosition[0].transform.position.x);
			spawnZLocation = Mathf.RoundToInt(playersPosition[0].transform.position.z + randomZSpawnDistance);

			//checking if these numbers are off-limits
			if (spawnXLocation <= 405 && spawnXLocation >= 20 && spawnZLocation >= 13 && spawnZLocation <= 420) {
				zombie = Instantiate(zombieTier1, new Vector3(spawnXLocation, 30, spawnZLocation), Quaternion.identity);
				zombie.name = "1zombie" + loopCounter;
				loopCounter += 1;
			}
			yield return new WaitForSeconds(0.1f); //I need this because a coroutine needs a return value
		}
	}

	private void OnCollisionEnter(Collision collision) {
		Destroy(gameObject.GetComponent<Rigidbody>());
	}
}
