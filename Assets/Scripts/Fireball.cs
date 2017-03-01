using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
	public float speed = 10.0f;
	public int damage = 1;

	[SerializeField] private GameObject flamesPrefab;
	private GameObject _flames;

	// Use this for initialization
	void Start () {

		_flames = Instantiate (flamesPrefab) as GameObject;
		_flames.transform.position = transform.position;
		_flames.transform.rotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0.0f, 0.0f, speed * Time.deltaTime);
		_flames.transform.position = transform.position;
	}


	void OnTriggerEnter(Collider other){
		PlayerCharacter player = other.GetComponent<PlayerCharacter> ();
		if (player != null) {
			Debug.Log ("player hit!");
		}
		Destroy (_flames);
		Destroy (this.gameObject);

	}
}
