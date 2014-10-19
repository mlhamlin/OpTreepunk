using UnityEngine;
using System.Collections;

public class Lever : Activator {

	public GameObject player;
	public float switching;
	public bool isRight;
	public GameObject[] listeners;


	delegate void SwitchDelegate();
	SwitchDelegate switchDelegate;

	// set the type and add delegates
	void Start () {
		type = "lever";
		foreach (GameObject listener in listeners) {
			switchDelegate += listener.GetComponent<Listener>().notify;
		}
	}

	// reduce the time spent switching, if applicable
	void Update () {
		switching = Mathf.Max(switching-Time.deltaTime, 0);
	}

	// Activate this switch if it's not already switching
	public override void Activate() {
		if (switching <= 0)
		{
			switching = 0.5f;
			isRight = !isRight;
			switchDelegate();
		}
	}
}
