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
	
	void FixedUpdate () {
	}

	override public void performAction () {
		List<Collider2D> targets = targetSpace.getTargets();
		foreach(Collider2D col in targets)
		{
			if (col == null)
				return;

			HealthData health = findThatHealth(col.gameObject);

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

	private HealthData findThatHealth(GameObject g)
	{
		HealthData health = g.GetComponent<HealthData> ();
		GameObject current = g;
		if (health != null)
						return health;

		while (health == null && (current.transform.parent != null))
		{
			current = current.transform.parent.gameObject;
			health = current.GetComponent<HealthData> ();
		}

		return health;
	}
}
