﻿using UnityEngine;
using System.Collections;

public class CharPickUp : MonoBehaviour {
	CharInventory charInventory;
	bool holdPickup;

	void Start() {
		charInventory = transform.parent.GetComponent<CharInventory> ();
	}

	void Update() {
		if (!Input.GetButton ("Pickup") && holdPickup) {
			Debug.Log ("Let go of Pickup button");
			holdPickup = false;
		}
		if (Input.GetButton ("Pickup") && !holdPickup && charInventory.isHoldingItem ()) { //calls on drop method.
			Debug.Log ("Call on drop.\nButton = " + Input.GetButton ("Pickup") + ". holdPickup = " + holdPickup + ". isholding = " + charInventory.isHoldingItem());
			holdPickup = true;
			charInventory.getHoldingItem ().GetComponent<PickUpableItem> ().Dropped ();
			charInventory.setHoldingItem (null);
		}
	}
		
	void OnTriggerStay2D(Collider2D col) {
		if (Input.GetButton ("Pickup") && !holdPickup && !charInventory.isHoldingItem ()) {
			holdPickup = true;
			Debug.Log ("Tried to pick up " + col.gameObject);
			Debug.Log ("holdPickup = " + holdPickup);
			switch (col.gameObject.tag) {

			case "rock":
				charInventory.setHoldingItem (col.gameObject);
				col.gameObject.GetComponent<PickUpableItem> ().PickedUp (this.gameObject);
				break;

			case "branch":
				charInventory.setHoldingItem (col.gameObject);
				col.gameObject.GetComponent<PickUpableItem> ().PickedUp (this.gameObject);
				break;
			}
		}
	}
}
