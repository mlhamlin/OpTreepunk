using UnityEngine;
using System.Collections;

public class HealthData : MonoBehaviour {

	int health;
	public bool alive;

	// Use this for initialization
	void Start () {
		alive = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Damage(int amount) {
		health = Mathf.Min (0, health - amount);
		alive = !(health == 0);
	}

	public void Heal(int amount){
		if (alive) 
		{
			health = Mathf.Min (0, health - amount);
		}
	}
}
