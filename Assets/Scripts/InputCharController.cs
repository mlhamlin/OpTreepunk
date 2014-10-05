﻿using UnityEngine;
using System.Collections;

public class InputCharController : MonoBehaviour {

	private Character thisCharacter;
	public string HorizontalAxis;
	public string JumpAxis;
	public string AttackAxis;

	// Use this for initialization
	void Start () {
		thisCharacter = GetComponent<Character>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		float vel = Input.GetAxis(HorizontalAxis);
        thisCharacter.SetMove(vel);

		if (Input.GetAxis(JumpAxis) > 0)
		{
			thisCharacter.Jump();
		}

		if ((!AttackAxis.Equals("")) && (Input.GetAxis(AttackAxis) > 0))
		{
			thisCharacter.Attack();
		}
	}
}
