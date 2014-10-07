using UnityEngine;
using System.Collections;

public class HealthData : MonoBehaviour {

	public int health;
	private bool alive;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator>();
		alive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!alive)
		{
			anim.SetTrigger("Die");
			Invoke("Fade",30);
		}
	}

	public void Damage (int amount) {
		health = Mathf.Max (0, health - amount);
		alive = !(health == 0);
	}

	public void Heal (int amount){
		if (alive) 
		{
			health = Mathf.Min (0, health - amount);
		}
	}

	public void Fade (){
		anim.SetTrigger("Fade");
		Invoke("myDestroy",15);
	}

	public void myDestroy (){
		Destroy(gameObject);
	}
}
