using UnityEngine;
using System.Collections;

public class HealthData : MonoBehaviour {

	public int health;
	public int maxhealth;
	private bool alive;
	private bool hasDied;
	private Character thisCharacter;

	// Use this for initialization
	void Start () {
		thisCharacter = GetComponent<Character>();
		alive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!alive && !hasDied)
		{
			hasDied = true;
			thisCharacter.Kill();
		}
	}

	public void Damage (int amount) {
		if (!alive)
			return;

		health = Mathf.Max (0, health - amount);
		alive = !(health == 0);
	}

	public void Heal (int amount){
		if (alive) 
		{
			health = Mathf.Min (0, health - amount);
		}
	}

	public bool isDead ()
	{
		return !alive;
	}
	
}
