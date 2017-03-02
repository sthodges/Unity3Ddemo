using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent (typeof(Camera))]
public class RayShooter : MonoBehaviour {
	private Camera _camera;

	[SerializeField] AudioSource audioSource;
	[SerializeField] AudioClip shotSound;

	[SerializeField] Material GUIMaterial;

	//private AudioSource _audioSource;
	//private AudioClip _shotSound;

	private Material _crosshairsMaterial;

	// Use this for initialization
	void Start () {
		_camera = GetComponent<Camera> ();


	//	_audioSource = GetComponent<AudioSource> ();

		// add when doing onGUI
		//Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) &&
			!EventSystem.current.IsPointerOverGameObject() ) {

			audioSource.PlayOneShot (shotSound);
			
			Vector3 point = new Vector3 (_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0.0f);	
			Ray ray = _camera.ScreenPointToRay (point);
			RaycastHit hit;

			Debug.DrawRay (ray.origin, ray.direction);

			if (Physics.Raycast (ray, out hit)) {
				//Debug.Log ("Hit " + hit.point);

				;

				GameObject hitObject = hit.transform.gameObject;
				ReactiveTarget target = hitObject.GetComponent<ReactiveTarget> ();
				if (target != null) {
					target.reactToHit ();
					Messenger.Broadcast (GameEvent.ENEMY_HIT);
				} else {
					StartCoroutine (SphereIndicator (hit.point));
				}
			}

		}
	}

	// add after Debug.Log test
	private IEnumerator SphereIndicator(Vector3 pos){
		GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		sphere.transform.localScale = new Vector3 (0.25f, 0.25f, 0.25f);
		sphere.transform.position = pos;
		yield return new WaitForSeconds (1.0f);
		Destroy (sphere);

	}

	//Add after SphereCollider shooting ConnectionTesterStatus
	// an official function -- called to make a GUI
	void OnGUI() {

		//GUI.color = new Color32(255, 255, 255, 180);

		//GUIStyle crosshairStyle = new GUIStyle ();
		//crosshairStyle.fontSize = 48;
		//crosshairStyle.font.material = GUIMaterial;
		//Font myFont = Resources.GetBuiltinResource<Font>("Arial.ttf");	

		//crosshairStyle.font = myFont;

		//Font myFont = Resources.GetBuiltinResource<Font>("Arial.ttf");	


		int size = 48; //12
		float _posX = _camera.pixelWidth / 2 - size / 4;
		float _posY = _camera.pixelHeight / 2 - size / 2;
		//GUI.Label (new Rect (_posX, _posY, size, size), "*");
		GUI.Label (new Rect (_posX, _posY, size, size), "<color=green><size=40>+</size></color>"); //<color=green><size=40>+</size></color>");
		//GUI.Label(Rect(500,350,200,50),"<color=green><size=40>Lose</size></color>");

	}



}
