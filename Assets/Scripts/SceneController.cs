using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SceneController : MonoBehaviour {
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private GameObject wallPrefab;
	[SerializeField] private GameObject floorPrefab;

	private GameObject _enemy;

	private string _mazeText;
	private string _defaultMaze;
	private int _mazeSize;

	// note: wall prefabs are 1x by 10z
	// so to figure out center point for drawing wall, depending on position adjust by
	private float [] XWallOffset = { -5f, 0f, 5f, 0f };
	private float[] ZWallOffset = { 0f, 5f, 0f, -5f };

	// Use this for initialization
	void Start () {
		//GameObject wall;
		_defaultMaze = System.IO.File.ReadAllText ("Assets/mazesm.txt");
		_mazeText = "";
		_mazeSize = 5;
		_mazeText = _defaultMaze;
		ReallyLoadTheMaze (); //LoadMaze (_defaultMaze);
	}

	// test maze, then load if "okay"
	/*void LoadMaze(string mazeToTest){
		_mazeSize = 5;
		if (false) {

		} else {
			_mazeText = _defaultMaze;
			_mazeSize = 5;
			XWallOffset [0] = -_mazeSize;
			XWallOffset [2] = _mazeSize;
			ZWallOffset [1] = _mazeSize;
			ZWallOffset [3] = -_mazeSize;
			ReallyLoadTheMaze ();
		}
	}
	*/

	// loads from 
	private void ReallyLoadTheMaze(){

		XWallOffset [0] = -_mazeSize;
		XWallOffset [2] = _mazeSize;
		ZWallOffset [1] = _mazeSize;
		ZWallOffset [3] = -_mazeSize;


		GameObject floorTile;
		// create outer wall
		for (int i = 0; i < _mazeSize; i++) {
			// hack becuase rotation 90deg out of synce
			//okay
			drawExteriorWallAt (new Vector3 (i * 10.0f - 19.5f, 3.0f, -19.5f), 3); // top walls

			// okay
			drawExteriorWallAt (new Vector3 (i * 10.0f - 19.5f, 3.0f, 20.5f), 1); // bottom walls

			//okay -- maybe actually right?
			drawExteriorWallAt (new Vector3 (20.5f, 3.0f, i * 10.0f - 19.5f), 2); // left walls

			//okay -- do want -- maybe left or actually top
			drawExteriorWallAt (new Vector3 (-29.5f, 3.0f, i * 10.0f - 19.5f), 2); // right walls


		}


		// make the floor from 1x1 prefabs
		for (int i = 0; i < _mazeSize; i++) {
			for (int j = 0; j < _mazeSize; j++) {
				floorTile = Instantiate (floorPrefab) as GameObject;
				floorTile.transform.position = new Vector3 (i * 10.0f - 20.0f, 0.0f, j * 10.0f - 20.0f);

			}
		}


		int temp;
		// create inner walls
		// hardcoded to 5x5 maze encoded into length 25 maze string
		char [] maze = _mazeText.ToCharArray();
		string square;
		for (int i = 0; i < _mazeSize-1; i++) {
			for(int j=0; j<_mazeSize-1; j++){

				//{{int i = 0; int j = 0; // alt with for loops for upper left square only testing
				temp = maze [i * _mazeSize + j];
				square = _mazeText.Substring (i * (_mazeSize +1) + j, 1); // skip over newlines
				temp = int.Parse (square, System.Globalization.NumberStyles.HexNumber);
				//Debug.Log (square +  " " + i + "," + j + " is " + temp);
				if (hasRightWall(temp)){
						//drawWallAt (new Vector3 (i * 10.0f - 19.5f+ 5.0f, 3.0f, j * 10.0f - 19.5f +5.0f), 0);
					    drawWallAt (new Vector3 (i * 10.0f - 14.5f, 3.0f, j * 10.0f - 14.5f), 0);
				} //hRW
				if (hasDownWall(temp)){
					//drawWallAt (new Vector3 (i * 10.0f - 19.5f +5.0f, 3.0f, j * 10.0f - 20.5f - 4.5f), 1);
					//drawWallAt (new Vector3 (i * 10.0f - 19.5f +5.0f, 3.0f, j * 10.0f - 20.5f - 4.249f), 1);
					drawWallAt (new Vector3 (i * 10.0f - 14.5f, 3.0f, j * 10.0f - 24.749f ), 1);
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
		float angle = UnityEngine.Random.Range (0.0f, 360.0f);
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


	public void LoadMazeFromClipboard(){
		string temp = GUIUtility.systemCopyBuffer;

		string[] lines = temp.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

		int size = lines.Length;

		for (int i = 0; i < size; i++) {
			if (lines [i].Length != size) {
				Debug.Log ("Line " + i + " of Maze has Length " + lines [i].Length + " but Length " + size + "was expected");
				Debug.Log ("discarding new Maze");
				return;
			}

		}
		Debug.Log ("Loading " + size + " by " + size + " maze.");
			// n x n maze string verified!

		/*
			using (System.IO.StringReader reader = new System.IO.StringReader(input)) {
    string line = reader.ReadLine();
}
		*/

		//for (int i = 0; i < size; i++) {
		//	Debug.Log (i+lines[i]);
		//}
	}


}
