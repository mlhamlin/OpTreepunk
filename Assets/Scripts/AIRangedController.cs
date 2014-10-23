using UnityEngine;
using System.Collections;

public class AIRangedController : MonoBehaviour {

	private Character thisCharacter;
	public GameObject eye;
	public float eyeRotationOffset;
	public GameObject gun;
	public float gunRotationOffset;
	public GameObject trackingTarget;

	// Use this for initialization
	void Start () {
		thisCharacter = GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
		//have the sprites aim toward the player
		float angle = Vector2.Angle(Vector2.right, trackingTarget.transform.position - gameObject.transform.position);
		gun.transform.localRotation = Quaternion.AngleAxis(angle + gunRotationOffset, Vector3.forward);
		eye.transform.localRotation = Quaternion.AngleAxis(angle + eyeRotationOffset, Vector3.forward);

		//always try to attack the player
		thisCharacter.TriggerAction1();
	}
}
