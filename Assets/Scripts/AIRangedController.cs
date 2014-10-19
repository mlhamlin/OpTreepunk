using UnityEngine;
using System.Collections;

public class AIRangedController : MonoBehaviour {

	private Character thisCharacter;

	// Use this for initialization
	void Start () {
		thisCharacter = GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
		//always try to attack the player
		thisCharacter.TriggerAction1();
	}
}
