using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private GameObject wallPrefab;
	private GameObject _enemy;

	private string _mazeText;

	// note: wall prefabs are 1x by 10z
	// so to figure out center point for drawing wall, depending on position adjust by
	private float [] XWallOffset = { -5f, 0f, 5f, 0f };
	private float[] ZWallOffset = { 0f, 5f, 0f, -5f };

	// Use this for initialization
	void Start () {
		//GameObject wall;
		_mazeText = System.IO.File.ReadAllText ("Assets/mazesm.txt");
		//Debug.Log (_mazeText);
		//Debug.Log (_mazeText.Length);
		// add some walls from the prefab programatically
		// test code initial version:
		//for (int i = 0; i < 10; i++) {
		//	wall = Instantiate (wallPrefab) as GameObject;
		//	wall.transform.position = new Vector3 (i*5, 5.0f, i*5);
		//}
		// create outer wall
		for (int i = 0; i < 5; i++) {
			// hack becuase rotation 90deg out of synce
			//okay
			drawExteriorWallAt (new Vector3 (i * 10.0f - 19.5f, 3.0f, -19.5f), 3); // top walls

			// okay
			drawExteriorWallAt (new Vector3 (i * 10.0f - 19.5f, 3.0f, 20.5f), 1); // bottom walls

			//okay
			drawExteriorWallAt (new Vector3 (20.5f, 3.0f, i * 10.0f - 19.5f), 2); // left walls

			//okay
			drawExteriorWallAt (new Vector3 (-29.5f, 3.0f, i * 10.0f - 19.5f), 2); // right walls


		}


		int temp;
		// create inner walls
		// hardcoded to 5x5 maze encoded into length 25 maze string
		char [] maze = _mazeText.ToCharArray();
		string square;
		for (int i = 0; i < 5; i++) {
			for(int j=0; j<5; j++){
	
		//{{int i = 0; int j = 0; // alt with for loops for upper left square only testing
				temp = maze [i * 5 + j];
				square = _mazeText.Substring (i * 6 + j, 1); // skip over newlines
				temp = int.Parse (square, System.Globalization.NumberStyles.HexNumber);
				//Debug.Log (square +  " " + i + "," + j + " is " + temp);
				if (hasRightWall(temp)){

					// okay
					if (i != 5-1)
					drawWallAt (new Vector3 (i * 10.0f - 19.5f+ 5.0f, 3.0f, j * 10.0f - 19.5f +5.0f), 0);

				} //hRW
				if (hasDownWall(temp)){

					if (j != 5-1)
				drawWallAt (new Vector3 (i * 10.0f - 19.5f +5.0f, 3.0f, j * 10.0f - 20.5f - 5.0f), 1);


				}// hDW
				//Debug.Log("HERE");
			}//j
		}//i


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

	// direction 0 Right, 1 Down, 2 Left, 3 up
	private void drawWallAt(Vector3 position, int direction){
		GameObject wall;
		float thickness = 0.5f; //adjust coordinates for the thickness of the wall
		Vector3 adjustedPosition = new Vector3 (position.x + XWallOffset [direction]-thickness, position.y, position.z + ZWallOffset [direction]-thickness);
		wall = Instantiate (wallPrefab) as GameObject;
		wall.transform.position = adjustedPosition;
		//if (direction == 1 || direction == 3)
		if (direction == 0 || direction == 2)	
			wall.transform.Rotate (0.0f, 90.0f, 0.0f);

	}

	// direction 0 Right, 1 Down, 2 Left, 3 up
	// OMG: shameful hack -- but I probably won't go back and fix the reversed axis
	private void drawExteriorWallAt(Vector3 position, int direction){
		GameObject wall;
		float thickness = 0.5f; //adjust coordinates for the thickness of the wall
		Vector3 adjustedPosition = new Vector3 (position.x + XWallOffset [direction]-thickness, position.y, position.z + ZWallOffset [direction]-thickness);
		wall = Instantiate (wallPrefab) as GameObject;
		wall.transform.position = adjustedPosition;
		if (direction == 1 || direction == 3)
			wall.transform.Rotate (0.0f, 90.0f, 0.0f);

	}

}
