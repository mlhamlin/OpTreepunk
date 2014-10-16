using UnityEngine;
using System.Collections;

public class LeverTrigger : MonoBehaviour {

	public GameObject player;
	public bool readyToActivate;
	public float switching;
	public bool isRight;
	public GameObject[] listeners;


	delegate void SwitchDelegate();
	SwitchDelegate switchDelegate;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 1f);
		foreach (GameObject listener in listeners) {
			switchDelegate += listener.GetComponent<Listener>().notify;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Activate") == 0) {  }
		
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
		renderer.color = new Color (1f, 1f, 0.0f);

	}
	
	void OnTriggerExit2D(Collider2D coll) {
			gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 1f);
	}
}
