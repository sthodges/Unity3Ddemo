using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(CharacterController))]
public class FPSInput : MonoBehaviour {
	public float moveSpeed = 6.0f;
	public float gravity = -9.8f;

	private CharacterController _characterController;

	void Start () {
		_characterController = GetComponent<CharacterController> ();	
	}
	
	void Update () {
		float deltaX = Input.GetAxis ("Horizontal") * moveSpeed;
		float deltaZ = Input.GetAxis ("Vertical") * moveSpeed;
		//transform.Translate (deltaX, 0.0f, deltaZ); // unrestricted

		Vector3 movement = new Vector3 (deltaX, 0.0f, deltaZ);
		movement = Vector3.ClampMagnitude (movement, moveSpeed);
		//movement.y = gravity;

		movement *= Time.deltaTime;
		movement = transform.TransformDirection (movement);
		movement.y = gravity; // apply gravity after world transform so that mouselook doesn't affect gravity axis
		_characterController.Move (movement);

	}
}
