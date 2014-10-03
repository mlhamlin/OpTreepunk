using UnityEngine;
using System.Collections;

public class LightListener : MonoBehaviour, Listener {

	public delegate void MyDelegate();
	public MyDelegate myDelegate;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void notify(bool on) {
		if (on) {
			gameObject.GetComponent<Light> ().color = new Color (0.0f, 1.0f, 0.0f);
				} else {
			gameObject.GetComponent<Light> ().color = new Color (1.0f, 0.0f, 0.0f);
				}
	}
}
