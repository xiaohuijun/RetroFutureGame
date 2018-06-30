﻿using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour {
	PlayerStatus status;
	Rigidbody2D rb2D;
	InputManager input;
	public float jumpSpeed, secondJumpSpeed;
	public bool jumpDown, holdJump, secondJumpAcquired, hasSecondJumped;
	[SerializeField] bool hasJumped, jumpingBackward;

	void Start() {
		status = GetComponent<PlayerStatus>();
		rb2D = GetComponent<Rigidbody2D>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
	}

	void FixedUpdate() {
		if(!input.GetKey("jump") && (hasJumped || hasSecondJumped) && status.grounded && !holdJump) {
			hasJumped = false;
			hasSecondJumped = false;
		}
		//enable jump button when not holding button and on surface
		if(!input.GetKey("jump") && holdJump) {
			holdJump = false;
		}

		//jump down through platform when holding down and pressing jump
		if(input.GetAxis("Y") < -0.3f && input.GetAxis("ysign") < 0f) {
			if(input.GetKey("jump") && !holdJump && status.onPlatform) {
				Debug.Log("JUMPDOWN");
				jumpDown = true;
				holdJump = true;
				rb2D.velocity = new Vector2(rb2D.velocity.x, -1f);
				transform.GetChild(1).gameObject.SetActive(false);
			}
			if(jumpDown)
				rb2D.velocity = status.onPlatform ? new Vector2(rb2D.velocity.x, -1f) : rb2D.velocity;
		}
		else if(jumpDown) {
			transform.GetChild(1).gameObject.SetActive(true);
			jumpDown = false;
		}

		//swim in water
		else if(input.GetKey("jump") && status.inWater) {
			rb2D.velocity = rb2D.velocity.y < jumpSpeed / 2 ? new Vector2(rb2D.velocity.x, rb2D.velocity.y + (jumpSpeed / 20)) : rb2D.velocity;
			holdJump = true;
		}

		//jump when on surface and pressing jump
		else if(input.GetKey("jump") && !holdJump && status.grounded && !hasJumped) {
			rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
			hasJumped = true;
			holdJump = true;
		}
		//jump in air when have secondjump and has not secondjumped.
		else if(input.GetKey("jump") && secondJumpAcquired && !holdJump && !hasSecondJumped && !status.isSmall) {
			rb2D.velocity = new Vector2(rb2D.velocity.x, secondJumpSpeed);
			hasSecondJumped = true;
			holdJump = true;
		}

		//decrease vertical velocity if let go of jump early
		else if(!input.GetKey("jump") && hasJumped && rb2D.velocity.y > jumpSpeed / 2f)
			rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y / 1.5f);
	}
}
