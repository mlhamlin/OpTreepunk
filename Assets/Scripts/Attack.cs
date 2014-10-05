using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Attack : Action {

	public int damage;
	public TargetSpace targetSpace;
	public bool attackAll = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override public void performAction () {
		List<Collider2D> targets = targetSpace.getTargets();
		foreach(Collider2D col in targets)
		{
			HealthData health = col.GetComponent<HealthData>();
			if (health != null)
			{
				health.Damage(damage);
				if (!attackAll)
				{
					return;
				}
			}
		}
	}
}
