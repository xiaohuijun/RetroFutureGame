﻿using UnityEngine;
using System.Collections;

public class TutorialTrigger : MonoBehaviour {
	public string tutorial;
	[TextArea]
	public string tutorialText;

	TutorialManager manager;

	void Start() {
		manager = GameObject.Find("tutorialPanel").GetComponent<TutorialManager>();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag.Equals("Player")) {
			Debug.Log("Tutorial triggered");
			manager.DisplayTutorial(tutorial, tutorialText);
			Destroy(gameObject);
		}
	}
}
