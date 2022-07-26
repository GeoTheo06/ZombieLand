using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
	public gameManager1 gameManager;
	public int playerHealth;

	private void Start() {
		playerHealth = 1000;
	}

	private void Update() {
		if (playerHealth <= 0) {
			gameManager.gameOver = true;
		}
	}

}
