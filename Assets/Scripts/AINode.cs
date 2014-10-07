using UnityEngine;
using System.Collections;

public class AINode : MonoBehaviour {
	public AINode[] otherNodes;
	public bool waschecked = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Set all nodes to unchecked
	public void uncheck() {
		waschecked = false;
		foreach (AINode ain in otherNodes) {
			if (ain.waschecked)
				ain.uncheck();
		}
	}
}
