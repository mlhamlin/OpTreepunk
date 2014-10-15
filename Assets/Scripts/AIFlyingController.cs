using UnityEngine;
using System.Collections;

public class AIFlyingController : MonoBehaviour {

	private Character thisCharacter;
	public AINode target;

	// Use this for initialization
	void Start () {
		thisCharacter = GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
		//move left or right towards the target
		if (target.transform.position.x > rigidbody2D.transform.position.x) {
			thisCharacter.MoveRight();
		} else if (target.transform.position.x < rigidbody2D.transform.position.x) {
			thisCharacter.MoveLeft();
		}

		//always try to attack the player
		thisCharacter.TriggerAction1();
	}
}
