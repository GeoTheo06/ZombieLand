using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryGun : MonoBehaviour
{
	int batteryGunDamage;
	float batteryGunRange;
	float shootDelay;
	float timer;
	private void Start() {
		shootDelay = 0.2f;
		batteryGunDamage = 10;
		batteryGunRange = 100;
		timer = 0;
	}

	public Camera cam;

	void Shoot() {
		RaycastHit hitInfo;
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, batteryGunRange)) {

			if (hitInfo.transform.tag == "zombieTier1") {
				GameObject zombieHit = hitInfo.transform.gameObject;
				zombieHit.GetComponent<zombieManager>().zombieHealth -= batteryGunDamage;
				Debug.Log(zombieHit.name + ": " + batteryGunDamage + "/" + zombieHit.GetComponent<zombieManager>().zombieHealth);
			}

		}
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
