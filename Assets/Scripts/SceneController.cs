using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private GameObject wallPrefab;
	private GameObject _enemy;

	private string _mazeText;

	// Use this for initialization
	void Start () {
		GameObject wall;
		_mazeText = System.IO.File.ReadAllText ("Assets/mazesm.txt");
		//Debug.Log (maze);
		//Debug.Log (maze.Length);
		// add some walls from the prefab programatically
		// test code initial version:
		//for (int i = 0; i < 10; i++) {
		//	wall = Instantiate (wallPrefab) as GameObject;
		//	wall.transform.position = new Vector3 (i*5, 5.0f, i*5);
		//}
		// create outer wall
		for (int i = 0; i < 10; i++) {
			wall = Instantiate (wallPrefab) as GameObject;
			wall.transform.position = new Vector3 (i*5.0f-25.0f, 5.0f, -25.0f);
			wall.transform.Rotate (0.0f, 90.0f, 0.0f);

			wall = Instantiate (wallPrefab) as GameObject;
			wall.transform.position = new Vector3 (i*5.0f-25.0f, 5.0f, 25.0f);
			wall.transform.Rotate (0.0f, 90.0f, 0.0f);

			wall = Instantiate (wallPrefab) as GameObject;
			wall.transform.position = new Vector3 (-25.0f, 5.0f, i*5.0f-25.0f);

			wall = Instantiate (wallPrefab) as GameObject;
			wall.transform.position = new Vector3 (25.0f, 5.0f, i*5.0f-25.0f);
	
		}


		// hardcoded to 5x5 maze encoded into length 25 maze string



	}
	
	// Update is called once per frame
	void Update () {
	if (_enemy == null) {
		_enemy = Instantiate (enemyPrefab) as GameObject;
		_enemy.transform.position = new Vector3 (0.0f, 1.0f, 0.0f);
		float angle = Random.Range (0.0f, 360.0f);
		_enemy.transform.Rotate (0.0f, angle, 0.0f);
	}



	} // update

	private bool hasRightWall(int wValue){
		return (wValue & 1) > 0;
	}

	private bool hasDownWall(int wValue){
		return (wValue & 2) > 0;
	}

}
