using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	int damage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void doAttack (Transform targetLoc, HealthData targetHealth) {
		//Animate?
		//Move?
		//Whatever?
		targetHealth.Damage (damage);
	}
}
