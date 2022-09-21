using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryGun : MonoBehaviour
{
	int batteryGunDamage;
	float batteryGunRange;
	float shootDelay;
	float timer;

	ParticleSystem[] searchGunshot;
	ParticleSystem gunshot;

	private void Start() {
		shootDelay = 0.2f;
		batteryGunDamage = 20;
		batteryGunRange = 100;
		timer = 0;

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
		Vector3 camPosition = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z + 5f);
		RaycastHit hitInfo;
		if (Physics.Raycast(camPosition, cam.transform.forward, out hitInfo, batteryGunRange)) {

			if (hitInfo.transform.tag == "zombieTier1") {
				GameObject zombieHit = hitInfo.transform.gameObject;
				zombieHit.GetComponent<zombieManager>().zombieHealth -= batteryGunDamage; //i do this because there are many zombieTier1s but i want to change only the value of the zombie that i hit
				zombieHit.GetComponent<zombieManager>().isZombieHit = true;
				Debug.Log(zombieHit.name + ": " + batteryGunDamage + "/" + zombieHit.GetComponent<zombieManager>().zombieHealth);
			}
		}
		gunshot.Play();
	}

	bool stopCounting;
	private void FixedUpdate() {
		if (Input.GetKey(KeyCode.Mouse0)) {

				timer += Time.deltaTime;

			if (timer > shootDelay) {
				Shoot();
				timer = 0;
			}

		}
	}
}
