using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryGun : MonoBehaviour
{
	public float batteryGunDamage;
	public float batteryGunRange;

	private void Start() {
		batteryGunDamage = 10;
		batteryGunRange = 100;
	}

	public Camera cam;
	void Shoot() {
		RaycastHit hitInfo;
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, batteryGunRange)) {
			Debug.Log(hitInfo.transform.name);
		}
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			Shoot();
		}
	}
}
