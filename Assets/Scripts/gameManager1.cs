using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager1 : MonoBehaviour
{
	public bool gameOver;

	private void Update() {
		if (gameOver) {
			Debug.Log("Game Over");
		}
	}
}
