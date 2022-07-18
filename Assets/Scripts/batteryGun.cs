using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryGun : MonoBehaviour
{
	int batteryGunDamage;
	float batteryGunRange;

	private void Start() {
		batteryGunDamage = 10;
		batteryGunRange = 100;
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

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			Shoot();
		}
	}
}
