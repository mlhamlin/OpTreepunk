using UnityEngine;
using System.Collections;

public class AICharController : MonoBehaviour {

	private Character thisCharacter;
	public AINode nextNode;
	public AINode target;
	public GameObject raycastTarget;

	// Use this for initialization
	void Start () {
		thisCharacter = GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
		//reset the destination each frame
		setDestination();
		Debug.Log ("Going to Node: " + nextNode);

		float spacing = (nextNode == target) ? 1f : .1f;
		//move left or right towards the target
		if (nextNode.transform.position.x > (rigidbody2D.transform.position.x + spacing)) {
			thisCharacter.MoveRight();
		} else if (nextNode.transform.position.x < (rigidbody2D.transform.position.x - spacing)) {
			thisCharacter.MoveLeft();
		} else if (nextNode == target){
			if ((nextNode.transform.position.x > rigidbody2D.transform.position.x) != thisCharacter.FacingRight())
			{
				thisCharacter.Flip();
			}
			thisCharacter.TriggerAction1();
		}

		//jump if on the ground and the node is higher
		if (thisCharacter.Grounded () && nextNode.transform.position.y > rigidbody2D.transform.position.y) 
		{
			//Debug.Log ("Jump");
			thisCharacter.Jump ();
		}
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
		Debug.Log(Mathf.Sqrt(sqdist(rt, target.transform)) + " ," + Mathf.Sqrt(sqdist(target.transform, tt)));
		foreach (AINode ain in nodes) {
			//found a better node with a better distance sum
			if (Mathf.Sqrt(sqdist(rt, ain.transform)) + Mathf.Sqrt(sqdist(ain.transform, tt)) <
			    Mathf.Sqrt(sqdist(rt, nextNode.transform)) + Mathf.Sqrt(sqdist(nextNode.transform, tt))) {
				//if it's the target, raycast to make sure that it's reachable from here
				if (ain == target) {
					RaycastHit2D r = Physics2D.Raycast(rt.position, tt.position - rt.position, Mathf.Infinity, (1 << 8) | (1 << 9));
					//if the raycast just hits the target, it's fine to use as the best node
					if (r.collider.gameObject == raycastTarget) {
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
