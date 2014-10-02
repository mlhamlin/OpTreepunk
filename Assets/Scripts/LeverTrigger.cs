using UnityEngine;
using System.Collections;

public class LeverTrigger : MonoBehaviour {

	public GameObject player;
	int state = 0;

	// Use this for initialization
	void Start () {
		state = 0;
		gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		if ((state == 1 || state == 2) && Input.GetAxis ("Activate") == 1) {
			state = 2;
			gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
				} else if (state == 2) {
			state = 1;
			gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f);
				}
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		state = 1;
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
		renderer.color = new Color (1f, 1f, 0.0f);

	}
	
	void OnTriggerExit2D(Collider2D coll) {
			state = 0;
			gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 1f);
	}
}
