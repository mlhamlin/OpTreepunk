using UnityEngine;
using System.Collections;

public class LightListener : Listener {

	public delegate void MyDelegate();
	public MyDelegate myDelegate;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void notify(bool on) {
		print ("The light listener is doing this!");
		if (on) {
			gameObject.GetComponent<Light> ().color = new Color (0.0f, 1.0f, 0.0f);
				} else {
			gameObject.GetComponent<Light> ().color = new Color (1.0f, 0.0f, 0.0f);
				}
	}
}
