using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	[SerializeField] private Text scoreLabel;

	[SerializeField] private SettingsPopup settingsPopup;

	private int _score;

	// Use this for initialization
	void Start () {
		settingsPopup.Close ();
		_score = 0;
		scoreLabel.text = _score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		//scoreLabel.text = Time.realtimeSinceStartup.ToString ();	
		//scoreLabel.text = _score.ToString();
	
	}


	void Awake(){
		Messenger.AddListener (GameEvent.ENEMY_HIT, OnEnemyHit);
	}

	void OnDestroy(){
		Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
	}


	public void OnOpenSettings(){
		//Debug.Log ("open settings");
		settingsPopup.Open();
	}

	private void OnEnemyHit(){
		_score += 1;
		scoreLabel.text = _score.ToString();

	}
}
