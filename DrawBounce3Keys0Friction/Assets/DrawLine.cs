using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLine : MonoBehaviour {
	private LineRenderer lineRenderer;
	private EdgeCollider2D edgeCollider;
	private List<Vector3> listOfLinePoints;
	private List<Vector2> listOfEdgeColliderPoints;
	private Vector3 mousePos;

	private bool lineDone = false;
	private bool lineJustCreated = true;
	private bool isMousePressed;

	private float lineThickness = 0.2f; // thickness of line
	private float lineLifeTime = 2f; // time in seconds until line disappears (from when it had reached its maximum length)
	private string lineSortingLayerName = "Ball"; // same sorting layer as ball
	private string lineShaderMaterialName = "Sprites/Default"; // just using the default sprite for the line
	//private string edgeColliderMaterial = "LineColliderMaterial"; // the material for the line collider determines the friction!
			
	void Awake () {
		isMousePressed = false;
		listOfLinePoints = new List<Vector3> ();
		listOfEdgeColliderPoints = new List<Vector2> ();
		lineRenderer = gameObject.AddComponent<LineRenderer> ();
		edgeCollider = gameObject.AddComponent<EdgeCollider2D> ();
		lineRenderer.SetVertexCount (0);
		lineRenderer.useWorldSpace = true;
		lineRenderer.SetWidth (lineThickness, lineThickness);
		lineRenderer.sortingLayerName = lineSortingLayerName;
		lineRenderer.material = new Material (Shader.Find (lineShaderMaterialName));
		edgeCollider.sharedMaterial = new PhysicsMaterial2D ("LineColliderMaerial"); // material for the collider of the line
	}

	void Update () {
		if (lineDone) {
			Destroy (gameObject, lineLifeTime); //instead initialise destruction of line after lineLifeTime seconds
			return;
		}
		if (lineJustCreated) {
			lineJustCreated = false;
			isMousePressed = true;
		} 
		else if (Input.GetMouseButtonUp (0)) {
			isMousePressed = false;
			lineDone = true;
		}
		if (isMousePressed) {
			mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousePos.z = 0;
			if (!listOfLinePoints.Contains (mousePos)) { // check that mousePos is not already contained in list (e.g. still same as last frame)
				listOfLinePoints.Add (mousePos);
				listOfEdgeColliderPoints.Add (new Vector2 (mousePos.x, mousePos.y));
				if (listOfEdgeColliderPoints.Count > 2) {
					edgeCollider.points = listOfEdgeColliderPoints.ToArray ();
				}
				lineRenderer.SetVertexCount (listOfLinePoints.Count);
				lineRenderer.SetPosition (listOfLinePoints.Count - 1, (Vector3)listOfLinePoints [listOfLinePoints.Count - 1]);
			}
		}
	}
}