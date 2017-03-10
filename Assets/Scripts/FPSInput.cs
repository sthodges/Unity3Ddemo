using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(CharacterController))]
public class FPSInput : MonoBehaviour {
	public float moveSpeed = 6.0f;
	public float gravity = -9.8f;
	//public float lookDownCameraHeight = 70.0f;


	private bool flying;

	private float _vertSpeed;
	private Vector3 initCameraPosition;
	private Vector3 aboveCameraPosition;

	private CharacterController _characterController;


	void Start () {
		flying = false;
		_characterController = GetComponent<CharacterController> ();	


	}
	
	void Update () {
		if (!flying) {
			float deltaX = Input.GetAxis ("Horizontal") * moveSpeed;
			float deltaZ = Input.GetAxis ("Vertical") * moveSpeed;

			Vector3 movement = new Vector3 (deltaX, 0.0f, deltaZ);
			movement = Vector3.ClampMagnitude (movement, moveSpeed);
			movement.y = gravity;

			movement *= Time.deltaTime;
			movement = transform.TransformDirection (movement);		 
			_characterController.Move (movement);
		}
	
		if (Input.GetButtonDown ("Jump")) {
			Camera _myCamera = Camera.main;
			if (!flying) {
				
				initCameraPosition = _myCamera.transform.position;
				aboveCameraPosition = new Vector3 (initCameraPosition.x, initCameraPosition.y + 70.0f, initCameraPosition.z);
			 	iTween.MoveTo (_myCamera.gameObject, aboveCameraPosition, 1.0f);
				_myCamera.gameObject.transform.LookAt (this.gameObject.transform);

			} else {
				iTween.MoveTo (_myCamera.gameObject, initCameraPosition, 1.0f);
				//_myCamera.gameObject.transform.LookAt (this.gameObject.transform.forward)
			}
			flying = !flying;
		}






	}
}
