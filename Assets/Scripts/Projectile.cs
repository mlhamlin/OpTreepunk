using UnityEngine;
using System.Collections;

public class Projectile : TargetSpace {

	public LayerMask wall;
	public Action targetAction;

	// Use this for initialization
	void Start () {
		base.Start();
	}

	public void OnTriggerEnter2D(Collider2D enter) {
		//destroy this object if it hit the wall
		if (LayerInMask(wall, enter.gameObject.layer)) {
			Destroy(gameObject);
			return;
		}
		base.OnTriggerEnter2D(enter);
		//destroy this object if it hit its target
		if (LayerInMask(Layer, enter.gameObject.layer)) {
			targetAction.performAction();
			Destroy(gameObject);
		}
	}
}
