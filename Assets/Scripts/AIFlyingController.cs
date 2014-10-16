using UnityEngine;
using System.Collections;

public class AIFlyingController : MonoBehaviour {

	private Character thisCharacter;
	public AINode target;

	public float offsetTop;
	public float offsetBottom;
	public float pullAccel = 0.2f;
	public float pushAccel = 5;
	public float maxYSpeed = 5;

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

		//store the old Y velocity
		float oldVelocityY = rigidbody2D.velocity.y;
		oscillate();

		//try to attack the player when it's at its lowest point
		if (oldVelocityY < 0 && rigidbody2D.velocity.y >= 0)
			thisCharacter.TriggerAction1();
	}

	//function to simulate sine wave movement
	void oscillate() {
		Vector2 tp = transform.position;

		//stop y motion if there's a missing ceiling or floor
		RaycastHit2D top = Physics2D.Raycast(tp, new Vector2(0, 1), Mathf.Infinity, (1 << 8));
		RaycastHit2D bottom = Physics2D.Raycast(tp, new Vector2(0, -1), Mathf.Infinity, (1 << 8));
		if (bottom.collider == null || top.collider == null) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
			return;
		}

		//find the destination height, amplitude, top, and bottom
		float topY = top.point.y - offsetTop;
		float bottomY = bottom.point.y + offsetBottom;
		float centerY = (topY + bottomY) * 0.5f;
		float amplitude = (topY - bottomY) * 0.5f;

		//problem: enemy needs to make sine wave movements, but also fit the following:
		//    - be able to move gracefully into the sine wave from ANY y value
		//    - be able to start from 0 y velocity and quickly move into the sine wave
		//    - calculate its new velocity without knowing anything other than its current velocity and position
		//
		//solution: the top and bottom of the wave are "magnets" that change forces depending on where the enemy is
		//only the nearest magnet affects the enemy
		//magnets exert the following forces based on where the enemy is in relation to the center line and the magnet:
		//
		//1: enemy is between the two magnets but closer to the nearest magnet than to the center line:
		//    - enemy is pushed toward the other magnet like normal (force = X / distance)
		//2: enemy is closer to the center line than to the nearest magnet:
		//    2a: enemy is moving towards the nearest magnet:
		//        - enemy is pulled toward the nearest magnet with a constant force
		//    2b: enemy is moving away from the nearest magnet:
		//        - enemy is pushed toward the other magnet like normal (force = X / distance)
		//3: enemy is outside both the magnets (this will onlyh happen if the offsets are big enough):
		//    - enemy is pulled toward the nearest magnet with a constant force

		//find the distance to the center
		float distance = tp.y - centerY;
		float absDistance = Mathf.Abs(distance);

		//store stuff to figure out what the speed will be
		bool nearTop = distance > 0;
		float magnetY = nearTop ? topY : bottomY;
		float velocityY = rigidbody2D.velocity.y;
		float newSpeed;

		//3 and 2a: pull an enemy that is either far from the magnet or close to the center and moving towards the magnet
		if (absDistance > amplitude || (absDistance < amplitude * 0.5f && nearTop == (velocityY > 0)))
			newSpeed = velocityY + ((tp.y <= magnetY) ? pullAccel : -pullAccel);
		//1 and 2b: push an enemy that is close to the magnet or close to the center and moving away from the magnet
		else
			newSpeed = velocityY + (tp.y > magnetY ? pushAccel : -pushAccel) / Mathf.Pow(Mathf.Abs(tp.y - magnetY), 0.125f);

		if (Mathf.Abs(newSpeed) <= maxYSpeed)
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, newSpeed);
		else
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, newSpeed > 0 ? maxYSpeed : -maxYSpeed);
	}
}
