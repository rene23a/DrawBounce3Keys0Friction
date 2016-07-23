using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {
	private Rigidbody2D rigidBodyRef;
	private GameObject key1Obj;
	private GameObject key2Obj;
	private GameObject key3Obj;
	private GameObject exitDoorObj;
	private SpriteRenderer key1Sprite; // Ref to Key1 sprite
	private SpriteRenderer key2Sprite; // Ref to Key2 sprite
	private SpriteRenderer key3Sprite; // Ref to Key3 sprite
	private SpriteRenderer exitDoorSprite;
	private BoxCollider2D exitDoorCollider;
	private int ballInitialSpeed = 150; // initial speed of the ball
	private Vector2 ballInitialDirection = new Vector2 (1f, 0f); // initial direction of the ball
	private int keyCount; // count of how many keys have been unlocked

	void StartBall() {
		transform.position = new Vector3 (-4, 2, 0); // place the ball at the starting position again
		rigidBodyRef.velocity = new Vector2 (0f,0f); // first set velocity to 0, in case the ball was already moving
		rigidBodyRef.AddForce (ballInitialDirection * ballInitialSpeed); // apply a once-off force to the ball
		key1Sprite.color = Color.red;
		key2Sprite.color = Color.yellow;
		key3Sprite.color = Color.green;
		exitDoorSprite.color = Color.white;
		exitDoorCollider.enabled = true;
		keyCount = 0; // no keys have been unlocked yet
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (keyCount == 0 && other.gameObject.name == "Key1") {
			key1Sprite.color = Color.black;
			keyCount = 1;
		}
		if (keyCount == 1 && other.gameObject.name == "Key2") {
			key2Sprite.color = Color.black;
			keyCount = 2;
		}
		if (keyCount == 2 && other.gameObject.name == "Key3") {
			key3Sprite.color = Color.black;
			keyCount = 3;
			exitDoorSprite.color = Color.black;
			exitDoorCollider.enabled = false;
		}
		if (keyCount == 3 && other.gameObject.name =="Exit") { // if ball went thru exit, then start the level again
			Invoke ("Start", 5); // after 5s invoke the StartBall function again, which puts the ball in its initial position with velocity, force etc.
		}                            // NOTE: it also works to Invoke the Start function here instead of StartBall!
	}
		
	void Start () {
		rigidBodyRef = GetComponent<Rigidbody2D>();
		key1Obj = GameObject.Find ("Key1");
		key1Sprite = key1Obj.GetComponent<SpriteRenderer> ();
		key2Obj = GameObject.Find ("Key2");
		key2Sprite = key2Obj.GetComponent<SpriteRenderer> ();
		key3Obj = GameObject.Find ("Key3");
		key3Sprite = key3Obj.GetComponent<SpriteRenderer> ();
		exitDoorObj = GameObject.Find ("ExitDoor");
		exitDoorSprite = exitDoorObj.GetComponent<SpriteRenderer> ();
		exitDoorCollider = exitDoorObj.GetComponent<BoxCollider2D> ();
		StartBall (); // put ball in initial position, velocity=0, then apply initial force
	}

	void FixedUpdate () {
		Debug.Log (rigidBodyRef.velocity.magnitude);
		rigidBodyRef.velocity = rigidBodyRef.velocity.normalized * 3; // keeps the ball at a constant speed by manually re-setting it in FixedUpdate
		//if (rigidBodyRef.velocity.magnitude < 0.2f) {
		//	Invoke ("Start", 5);
		//}
	}
}
