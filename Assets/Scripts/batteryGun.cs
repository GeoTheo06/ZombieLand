using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryGun : MonoBehaviour {
	int batteryGunDamage;
	float batteryGunRange;
	float shootDelay;
	float continuousShootDelay;
	float timer;
	float timer1;


	bool playerHasShot;
	bool playerCanShoot;
	bool playerPendingToShoot;

	ParticleSystem[] searchGunshot;
	ParticleSystem gunshot;

	Collider RaycastCollider;

	private void Start() {
		shootDelay = 0.4f;
		continuousShootDelay = 0.3f;
		batteryGunDamage = 20;
		batteryGunRange = 100;
		timer = 0;
		timer1 = 0;
		RaycastCollider = GetComponent<Collider>();
		playerHasShot = false;
		playerCanShoot = true;
		playerPendingToShoot = false;

		//searching "gunshot" particle system by name
		searchGunshot = FindObjectsOfType<ParticleSystem>();
		for (int i = 0; i < searchGunshot.Length; i++) {
			if (searchGunshot[i].name == "gunshot") {
				gunshot = searchGunshot[i];
			}
		}

	}

	public Camera cam;

	void Shoot() {
		gunshot.Play();
		
		Vector3 camPosition = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z + 0.5f);
		
		RaycastHit hitInfo;
		
		if (Physics.Raycast(camPosition, cam.transform.forward, out hitInfo, batteryGunRange)) {

			if (hitInfo.transform.tag == "zombieTier1") {
				GameObject zombieHit = hitInfo.transform.gameObject; //the specific zombie player hit
				zombieHit.GetComponent<zombieManager>().zombieHealth -= batteryGunDamage; //i do this because there are many zombieTier1s but i want to change only the value of the zombie that i hit
				zombieHit.GetComponent<zombieManager>().isZombieHit = true;
				Debug.Log(zombieHit.name + ": " + batteryGunDamage + "/" + zombieHit.GetComponent<zombieManager>().zombieHealth);
			}
			
			Debug.DrawRay(camPosition, cam.transform.forward * batteryGunRange, Color.yellow);
		}
	}

	bool stopCounting;
	private void FixedUpdate() {
		//if the player is just pressing click once
		if (Input.GetKeyDown(KeyCode.Mouse0)) {

			if (playerCanShoot) {
				Shoot();
				playerHasShot = true;
			} else {
				//if player tries to shoot before the shootdelay
				playerHasShot = false; 
				playerPendingToShoot = true;
			}
		}

		if (playerHasShot) {

			playerCanShoot = false;
			
			timer += Time.deltaTime;

			if (timer >= shootDelay) {
				timer = 0;
				playerCanShoot = true;
				playerHasShot = false;
			}
		}

		//if the player presses click before the shootDelay time (it means that he is click spamming at least 2 times)
		if (playerPendingToShoot) {
			timer += Time.deltaTime;

			if (timer >= shootDelay) {
				Shoot();
				timer = 0;
				playerPendingToShoot = false;
				playerHasShot = true;
			}
		}

		//if player is pressing continuously click:
		if (Input.GetKey(KeyCode.Mouse0)) {

			timer1 += Time.deltaTime;

			if (timer1 > continuousShootDelay) {
				Shoot();
				timer1 = 0;
			}

		}

		//if the player raised the click, it means that does not want to press it continuously - though the timer1 already started counting on the "continuously click pressing" code so i have to reset it. Else, the player will have way more clicks (in a tiny amount of time) than he should)
		if (Input.GetKeyUp(KeyCode.Mouse0)) {
			timer1 = 0;
		}
	}
}
