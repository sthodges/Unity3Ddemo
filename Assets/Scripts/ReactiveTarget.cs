using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour {

	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip dieSound;

	private bool _alive;

	// Use this for initialization
	void Start () {

		Rigidbody body = GetComponent<Rigidbody> ();
		if (body != null) {
			body.freezeRotation = true;
		}

		_alive = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void reactToHit(){
		// added after WanderingAI

		if (_alive) {
			audioSource.PlayOneShot (dieSound);

			WanderingAI behavior = GetComponent<WanderingAI>();
			if (behavior != null) {
				behavior.SetAlive (false);
			}

			Rigidbody body = GetComponent<Rigidbody> ();
			if (body != null) {
				body.freezeRotation = false;
			}


			StartCoroutine (Die ());

		}

		_alive = false;




	}

	private IEnumerator Die(){



		this.transform.Rotate (-75, 0, 0);
		yield return new WaitForSeconds (1.5f);
		Destroy (this.gameObject);
	
	}
}
