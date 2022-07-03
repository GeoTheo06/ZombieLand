using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
	public float mouseSensitivity;
	public Transform playerBody;

	float xRotation = 0f;
	private void Start() {
		mouseSensitivity = 100;
		Cursor.lockState = CursorLockMode.Locked;
	}
	private void Update() {
		
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
		playerBody.Rotate(Vector3.up * mouseX);

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90, 40);

		transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
	}

}
