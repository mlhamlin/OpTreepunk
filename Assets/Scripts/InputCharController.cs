using UnityEngine;
using System.Collections;

public class InputCharController : MonoBehaviour {

	private Character thisCharacter;
	public string HorizontalAxis;
	public string JumpAxis;
	public string ActionOneAxis;
	public string ActionTwoAxis;

	// Use this for initialization
	void Start () {
		thisCharacter = GetComponent<Character>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		float vel = Input.GetAxis(HorizontalAxis);
		if (vel > 0)
		{
			thisCharacter.MoveRight();
		} else if (vel < 0) {
			thisCharacter.MoveLeft();
		}

		if (Input.GetAxis(JumpAxis) > 0)
		{
			thisCharacter.Jump();
		}

		if ((!ActionOneAxis.Equals("")) && (Input.GetAxis(ActionOneAxis) > 0))
		{
			thisCharacter.TriggerAction1();
		}

		if ((!ActionTwoAxis.Equals("")) && (Input.GetAxis(ActionTwoAxis) > 0))
		{
			thisCharacter.TriggerAction2();
		}
	}
}
