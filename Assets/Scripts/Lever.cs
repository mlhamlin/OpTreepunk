using UnityEngine;
using System.Collections;

public class Lever : Activator {

	public float switching;
	public bool isRight;
	public GameObject[] listeners;


	delegate void SwitchDelegate();
	SwitchDelegate switchDelegate;

	// set the type and add delegates
	void Start () {
		type = "lever";
        if (listeners.Length == 0)
        {
            switchDelegate = () => { };
        }
		foreach (GameObject listener in listeners) {
			switchDelegate += listener.GetComponent<Listener>().notify;
		}
	}

	// reduce the time spent switching, if applicable
	void Update () {
		switching = Mathf.Max(switching-Time.deltaTime, 0);
	}

    void flip()
    {
        isRight = !isRight;
        Vector3 scale = transform.localScale;
        scale.x = -1 * scale.x;
        transform.localScale = scale;
    }

	// Activate this switch if it's not already switching
	public override void Activate() {
		if (switching <= 0)
		{
			switching = 0.5f;
            flip();
			switchDelegate();
		}
	}
}
