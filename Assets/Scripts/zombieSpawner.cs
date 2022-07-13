using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieSpawner : MonoBehaviour
{
	GameObject[] playersPosition;
	public GameObject zombieTier1;

	int loopCounter = 0;
	int makeXNegative;
	int makeZNegative;

	int randomXSpawnDistance;
	int randomZSpawnDistance;
	int playerAndZombieSpawnPositionsDistance;
	int zombieCountSpawn;

	private void Start() {
		gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
		playersPosition = GameObject.FindGameObjectsWithTag("Player");
		playerAndZombieSpawnPositionsDistance = 100;
		zombieCountSpawn = 100;
		StartCoroutine(zombiesSpawn());
	}

	IEnumerator zombiesSpawn() {
		while (loopCounter < zombieCountSpawn) {
			randomXSpawnDistance = UnityEngine.Random.Range(1, playerAndZombieSpawnPositionsDistance);
			randomZSpawnDistance = playerAndZombieSpawnPositionsDistance - randomXSpawnDistance;

			makeXNegative = UnityEngine.Random.Range(0, 2);
			makeZNegative = UnityEngine.Random.Range(0, 2);

			if (makeXNegative == 1) {
				randomXSpawnDistance = randomXSpawnDistance * -1;
			}
			if (makeZNegative == 1) {
				randomZSpawnDistance = randomZSpawnDistance * -1;
			}

			//checking if these numbers are off-limits
			if (playersPosition[0].transform.position.x + randomXSpawnDistance <= 410 && playersPosition[0].transform.position.x + randomXSpawnDistance >= 20 && playersPosition[0].transform.position.z + randomZSpawnDistance >= 13 && playersPosition[0].transform.position.z + randomZSpawnDistance <= 420) {
				Instantiate(zombieTier1, new Vector3(playersPosition[0].transform.position.x + randomXSpawnDistance, 30, playersPosition[0].transform.position.z + randomZSpawnDistance), Quaternion.identity);
				yield return new WaitForSeconds(0.1f); //I need this because a coroutine needs a return value
				loopCounter += 1;
			}
		}
	}

	private void OnCollisionEnter(Collision collision) {
		Destroy(gameObject.GetComponent<Rigidbody>());
		gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
	}
}
