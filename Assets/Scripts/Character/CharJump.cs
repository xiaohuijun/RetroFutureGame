﻿using UnityEngine;
using System.Collections;

public class CharJump : MonoBehaviour {
	CharStatus status;
	Rigidbody2D rigidBody2D;
	public float jumpSpeed;
	public bool jumpDown, hasJumped, holdJump;

	void Start () {
		status = GetComponent<CharStatus> ();
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}
		
	void FixedUpdate () {
		if (Input.GetAxis ("Jump") == 0 && holdJump) {
			holdJump = false;
		}
		if (Input.GetAxis ("Jump") > 0 && Input.GetAxis ("Vertical") < 0 && status.onPlatform) {
			jumpDown = true;
		} else if (Input.GetAxis ("Jump") > 0 && !holdJump && (status.onGround || status.onPlatform)) {
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, jumpSpeed);
			hasJumped = true;
			holdJump = true;
		} else if (Input.GetAxis ("Jump") == 0 && hasJumped && rigidBody2D.velocity.y > jumpSpeed / 2) {
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, jumpSpeed / 2);
		}
	}
}
