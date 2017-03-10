using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseLook : MonoBehaviour {

	public enum AllowedRotationAxes {
		None = 0,
		MouseXY = 3,
		MouseX = 1,
		MouseY = 2
	}

	public AllowedRotationAxes axes = AllowedRotationAxes.MouseXY;

	public float sensitivityHorizontal = 9.0f;
	public float sensitivityVertical = 9.0f;
	public float minimumVertical = -85.0f;
	public float maximumVertical = 85.0f;

	private float _rotationX = 0.0f;

	// Use this for initialization
	void Start () {
		Rigidbody body = GetComponent<Rigidbody> ();
		if (body != null) {
			body.freezeRotation = true;
		}


		
	}
	
	// Update is called once per frame
	void Update () {
		if (axes == AllowedRotationAxes.MouseX) {
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHorizontal, 0);
		} // MouseX
		else if (axes == AllowedRotationAxes.MouseY) {
			_rotationX -= Input.GetAxis("Mouse Y") * sensitivityVertical;
			_rotationX = Mathf.Clamp(_rotationX, minimumVertical, maximumVertical);

			transform.localEulerAngles = new Vector3(_rotationX, transform.localEulerAngles.y, 0);
		} // MouseY
		else if (axes == AllowedRotationAxes.MouseXY) {
			_rotationX -= Input.GetAxis ("Mouse Y") * sensitivityVertical;
			_rotationX = Mathf.Clamp (_rotationX, minimumVertical, maximumVertical);

			float delta = Input.GetAxis ("Mouse X") * sensitivityHorizontal;
			float rotationY = transform.localEulerAngles.y + delta;

			transform.localEulerAngles = new Vector3 (_rotationX, rotationY, 0.0f);
		} // MouseXY
	}
}

/*
 * 
 * if (axes == RotationAxes.MouseX) {
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
		}
		else if (axes == RotationAxes.MouseY) {
			_rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
			_rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
			
			transform.localEulerAngles = new Vector3(_rotationX, transform.localEulerAngles.y, 0);
		}
		else {
			float rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityHor;

			_rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
			_rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

			transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
		}

*/