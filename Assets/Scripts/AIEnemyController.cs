using UnityEngine;
using System.Collections;

public class AIEnemyController : MonoBehaviour {
	public AINode nextNode;
	public float acceleration;
	public float maxSpeed;
	public AINode target;
	GameObject player;

	public Transform groundCheck;
	float groundRadius = 0.001f;
	public LayerMask whatIsGround;
	public float jumpvelocity;

	// Use this for initialization
	void Start () {
		player = target.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		//reset the destination each frame
		setDestination();

		//move left or right towards the target
		if (nextNode.transform.position.x > rigidbody2D.transform.position.x) {
			accelerate(acceleration);
		} else {
			accelerate(-acceleration);
		}

		//jump if on the ground and the node is higher
		if (Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround) &&
		    nextNode.transform.position.y > rigidbody2D.transform.position.y)
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpvelocity);
	}

	//accelerate the given amount up to a max velocity in either direction
	void accelerate(float amount) {
		float newvelocity = rigidbody2D.velocity.x + amount;
		if (Mathf.Abs(newvelocity) <= maxSpeed)
			rigidbody2D.velocity = new Vector2(newvelocity, rigidbody2D.velocity.y);
	}

	//set the nextNode to be the target for movement
	void setDestination() {
		//find the new nearest node, then uncheck all of them
		nextNode = getNearestNode(nextNode);
		nextNode.uncheck();

		//if the nearest node is the target, stop here
		if (nextNode == target)
			return;

		//find the best node to go to
		//the nodes are ranked by the sum of the distances between
		//    - enemy and the node
		//    - node and the player
		//lowest sum is the best node
		AINode[] nodes = nextNode.otherNodes;
		Transform rt = rigidbody2D.transform;
		Transform tt = target.transform;
		foreach (AINode ain in nodes) {
			//found a better node with a better distance sum
			if (Mathf.Sqrt(sqdist(rt, ain.transform)) + Mathf.Sqrt(sqdist(ain.transform, tt)) <
			    Mathf.Sqrt(sqdist(rt, nextNode.transform)) + Mathf.Sqrt(sqdist(nextNode.transform, tt))) {
				//if it's the target, raycast to make sure that it's reachable from here
				if (ain == target) {
					RaycastHit2D r = Physics2D.Raycast(rt.position, tt.position - rt.position, Mathf.Infinity, (1 << 8) | (1 << 9));
					//if the raycast just hits the target, it's fine to use as the best node
					if (r.collider.gameObject == player) {
						nextNode = target;
						return;
					}
				} else
					nextNode = ain;
			}
		}
	}

	//recur through the node map to find the nearest node
	AINode getNearestNode(AINode node) {
		AINode[] nodes = node.otherNodes;
		Transform rt = rigidbody2D.transform;
		foreach (AINode ain in nodes) {
			//replace the local node with the new nearest node
			if (!ain.waschecked && sqdist(ain.transform, rt) < sqdist(node.transform, rt)) {
				ain.waschecked = true;
				node = getNearestNode(ain);
			//make sure it's marked as checked even if it wasn't shorter
			} else {
				ain.waschecked = true;
			}
		}
	    return node;
	}

	//determine the distance squared between this and the node
	float sqdist(Transform t1, Transform t2) {
		float distx = t1.position.x - t2.position.x;
		float disty = t1.position.y - t2.position.y;
		return distx * distx + disty * disty;
	}
}
