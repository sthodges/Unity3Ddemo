using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	[SerializeField] private Text scoreLabel;

	[SerializeField] private SettingsPopup settingsPopup;

	// Use this for initialization
	void Start () {
		settingsPopup.Close ();
	}
	
	// Update is called once per frame
	void Update () {
		scoreLabel.text = Time.realtimeSinceStartup.ToString ();	
	}


	public void OnOpenSettings(){
		//Debug.Log ("open settings");
		settingsPopup.Open();
	}

}
