using UnityEngine;
using System.Collections;

public class CharStomp : MonoBehaviour {	
	Rigidbody2D rigidBody2D;
	CharStatus charStatus;
	public bool isStomping, groundStomping = false;
	bool holdStomp;
	StompTrigger stompTrigger;

	// Use this for initialization
	void Start () {
		rigidBody2D = GetComponent<Rigidbody2D> ();
		charStatus = GetComponent<CharStatus> ();
		stompTrigger = GameObject.Find ("stompTrigger").GetComponent<StompTrigger> ();
		//Change sprite, display correct tutorial and play theme.
	}

	void Update() {
		if (!Input.GetButton ("Attack"))
			holdStomp = false;
		stompTrigger.enabled = groundStomping;
	}
	
	void FixedUpdate() {
		if (charStatus.InAir () && !charStatus.IsFloating && Input.GetAxis ("Vertical") < -0.7 && Input.GetButton ("Attack") && !holdStomp) {
			holdStomp = true;
			isStomping = true;
			rigidBody2D.velocity = new Vector2 (0, 0);
			rigidBody2D.gravityScale = 0.0f;
			//Play stomp-animation
			Invoke ("Stomp", 0.5f);
		} else if (charStatus.onSurface) {
			groundStomping = true;
			Invoke ("FinishedStomp", 1f);
		}
	}

	void FinishedStomp() {
		isStomping = false;
		groundStomping = false;
	}

	void Stomp() {
		rigidBody2D.gravityScale = 2.0f;
		rigidBody2D.velocity = new Vector2 (0, -9f);
	}	
}