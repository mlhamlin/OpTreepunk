using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChargeAttack: Action {

	public int damage;
	public TargetSpace targetSpace;
	public bool attackAll;
	public float impulse;
	public Character character;

	override public void performAction () {
		List<Collider2D> targets = targetSpace.getTargets();

		foreach(Collider2D col in targets)
		{
			if (col == null)
				continue;

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

		int facing = character.FacingRight () ? 1 : -1;
		Vector2 force = new Vector2 (impulse * facing, 0);
		gameObject.rigidbody2D.AddRelativeForce (force);
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
