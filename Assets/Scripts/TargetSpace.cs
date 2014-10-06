﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetSpace : MonoBehaviour {

	List<Collider2D> targets;
	LayerMask Layer;

	// Use this for initialization
	void Start () {
		targets = new List<Collider2D>();	
	}

	public void OnTriggerEnter2D(Collider2D enter) {
		targets.Add(enter);
	}

	public void OnTriggerExit2D(Collider2D leave) {
		targets.Remove(leave);
	}

	public List<Collider2D> getTargets()
	{
		return targets;
	}
}