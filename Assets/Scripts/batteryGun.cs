using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryGun : MonoBehaviour
{
	public float batteryGunDamage;
	public float batteryGunrange;

	private void Start() {
		batteryGunDamage = 10;
		batteryGunrange = 100;
	}

	public Camera cam;
	void Shoot() {
		RaycastHit hitInfo;
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, range)) {
			Debug.Log(hitInfo.transform.name);
		}
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			Shoot();
		}
	}
}
