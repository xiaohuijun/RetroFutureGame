﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueBossPlayerAim : MonoBehaviour {
	GameObject player;
	public bool aim = true, raging;

	void Start () {
		player = GameObject.Find ("Char");
	}

	void Update () {
		if (raging && aim)
			transform.position = new Vector2 (
				Mathf.Lerp (transform.position.x, player.transform.position.x, Time.deltaTime),
				Mathf.Lerp (transform.position.y, player.transform.position.y, Time.deltaTime)
			);
	}
}
