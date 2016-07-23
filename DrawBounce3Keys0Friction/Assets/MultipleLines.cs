using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultipleLines : MonoBehaviour {
	private List<GameObject> listOfGameObjectLines; // this list will hold game objects, each of which will hold a line

	void Start () {
		listOfGameObjectLines = new List<GameObject>();
	}

	void Update () {
		if(Input.GetMouseButtonDown(0)){ // if mouse botton was pressed in this frame, then create a new line
			GameObject newLine = new GameObject("line"+listOfGameObjectLines.Count.ToString()); // create a new game object
			DrawLine newLineRef = newLine.AddComponent<DrawLine>(); // attach the DrawLine script to it, so that it can draw the line
			listOfGameObjectLines.Add(newLine); // add the game object to the list of game objects, i.e. the list of lines
		}
	}
}