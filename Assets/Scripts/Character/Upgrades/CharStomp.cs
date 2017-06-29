using UnityEngine;
using System.Collections;

public class CharStomp : MonoBehaviour {	
	Rigidbody2D rigidBody2D;
	CharStatus status;
	InputManager input;

	public float knockForce;
	bool holdStomp, isStomping;
	public LayerMask whatIsHurtable;
	public Transform stompCenter;
	Collider2D[] victims;

	// Use this for initialization
	void Start () {
		rigidBody2D = GetComponent<Rigidbody2D> ();
		status = GetComponent<CharStatus> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
		//Change sprite, display correct tutorial and play theme.
	}

	void FixedUpdate() {
		if (!input.GetKey ("attack"))
			holdStomp = false;
		if (status.InAir () && !status.isSmall && !status.isFloating && input.GetAxis ("Y") < -0.3f && input.GetAxis ("Ybool") < 0f && input.GetKey ("attack") && !holdStomp) {
			holdStomp = true;
			isStomping = true;

			StartCoroutine (Stomp ());
		}
		else if (status.grounded && isStomping) {
			
			FinishStomp ();
		}
	}


	IEnumerator Stomp() {
		Debug.Log ("Started Stomp");
		rigidBody2D.velocity = new Vector2 (0, 0);
		rigidBody2D.gravityScale = 0.0f;

		yield return new WaitForSeconds (0.2f);
		status.invulnerable = true;
		rigidBody2D.gravityScale = 2.0f;
		rigidBody2D.velocity = new Vector2 (0, -9f);
	}

	void FinishStomp() {
		Debug.Log ("Finished Stomp");
		GetComponent<AudioPlayer> ().PlayClip (5, 2f);
		isStomping = false;
		status.Invulnerable (0.2f);

		victims = Physics2D.OverlapBoxAll (stompCenter.position, new Vector2 (4f, 2f), 0, whatIsHurtable);

		foreach (Collider2D victim in victims) {
			switch (victim.gameObject.tag) {

			case "SmallCritter":
				victim.gameObject.GetComponent<SmallCritter> ().TakeDamage (3);
				victim.gameObject.GetComponent<EnemyKnockback> ().Knockback (GameObject.Find ("Char"), knockForce);
				break;

			case "JumpingCritter":
				victim.gameObject.GetComponent<JumpingCritter> ().TakeDamage (3);
				victim.gameObject.GetComponent<EnemyKnockback> ().Knockback (GameObject.Find ("Char"), knockForce);
				break;

			case "CrawlerCritter":
				Debug.Log ("Hit crawler!");
				//Really bad code, should be re-written
				CrawlerCritter crawlerCritter = victim.gameObject.GetComponent<CrawlerCritter> ();
				if (!crawlerCritter.deShelled) {
					crawlerCritter.TakeDamage (1);
				} else if (crawlerCritter.deShelled) {
					crawlerCritter.TakeDamage (2);
				} 
				break;

			case "ShellMan":
				ShellMan shellMan = victim.gameObject.GetComponent<ShellMan> ();
				if (!shellMan.deShelled) {
					shellMan.TakeDamage (1);
				} else if (shellMan.deShelled) {
					shellMan.TakeDamage (2);
				}
				break;

			case "FinalBossLastForm":
				victim.gameObject.GetComponent<Phase3Head> ().TakeDamage ();
				break;
			}
			Debug.Log ("STOMPED: " + victim.gameObject.name + " with tag: " + victim.gameObject.tag);
		}
	}
}