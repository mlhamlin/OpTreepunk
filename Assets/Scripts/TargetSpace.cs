﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetSpace : MonoBehaviour {

	List<Collider2D> targets;
	public LayerMask Layer;

	// Use this for initialization
	protected void Start () {
		targets = new List<Collider2D>();	
	}

	public void OnTriggerEnter2D(Collider2D enter) {

		if (LayerInMask(Layer, enter.gameObject.layer))
		{
			if (!targets.Contains(enter))
			{
				targets.Add(enter);
			}
		}


	}

	public void OnTriggerExit2D(Collider2D leave) {
		targets.Remove(leave);
	}

	public List<Collider2D> getTargets()
	{
		return targets;
	}

	protected bool LayerInMask(LayerMask mask, int layer)
	{
		return mask.value == 1<<layer;
	}
}
