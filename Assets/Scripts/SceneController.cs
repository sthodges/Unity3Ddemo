using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {
	[SerializeField] private GameObject enemyPrefab;
	private GameObject _enemy;

	private string _mazeText;

	// Use this for initialization
	void Start () {
		_mazeText = System.IO.File.ReadAllText ("Assets/mazesm.txt");
		//Debug.Log (maze);
		//Debug.Log (maze.Length);
	}
	
	// Update is called once per frame
	void Update () {
	if (_enemy == null) {
		_enemy = Instantiate (enemyPrefab) as GameObject;
		_enemy.transform.position = new Vector3 (0.0f, 1.0f, 0.0f);
		float angle = Random.Range (0.0f, 360.0f);
		_enemy.transform.Rotate (0.0f, angle, 0.0f);
	}
}
}
