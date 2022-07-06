using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
	public float damage = 10;
	public float range = 100;

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
