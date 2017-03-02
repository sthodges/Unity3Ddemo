using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour {
	public float baseWanderingSpeed = 3.0f;
	public float wanderingSpeed;
	public float moveFactor = 1.0f;
	public float obstacleRange = 5.0f;

	[SerializeField] private GameObject fireballPrefab;
	private GameObject _fireball;

	private bool _alive;

	// Use this for initialization
	void Start () {
		_alive = true;
		_fireball = null;
		wanderingSpeed = baseWanderingSpeed * moveFactor;

	}
	
	void Update () {
		// move forward every frame if alive
		if (_alive) {
			transform.Translate (0.0f, 0.0f, wanderingSpeed * Time.deltaTime);
			RaycastHit hit;
			Ray ray = new Ray (transform.position, transform.forward);

			if (Physics.SphereCast (ray, 0.75f, out hit)) {
				GameObject hitObject = hit.transform.gameObject;

				if (hitObject.GetComponent<PlayerCharacter> ()) {
					if (_fireball == null) {
						_fireball = Instantiate (fireballPrefab) as GameObject;
						_fireball.transform.position = transform.TransformPoint (Vector3.forward * 1.5f);
						_fireball.transform.rotation = transform.rotation;

					}
				} else 
				if (hit.distance < obstacleRange) {
					float angle = Random.Range (-110.0f, 110.0f);
					transform.Rotate (0.0f, angle, 0.0f);
				}
			}
		} // if alive
	}

	public void SetAlive(bool alive){
		_alive = alive;
	}



	void Awake(){
		Messenger<float>.AddListener (GameEvent.SPEED_CHANGED, OnSpeedChanged);
	}

	void OnDestroy(){
		Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
	}

	private void OnSpeedChanged(float value){
		moveFactor = value;
		wanderingSpeed = baseWanderingSpeed * moveFactor;
	}
}
