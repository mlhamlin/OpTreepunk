using UnityEngine;
using System.Collections;

public class SpawnProjectile : Action {

	public GameObject target;
	public float projectileSpeed;
	public GameObject Projectile;

	override public void performAction () {
		Vector2 tp = gameObject.transform.position;
		GameObject projectile = (GameObject) Instantiate(Projectile, tp, Quaternion.identity);
		Vector2 ttp = target.transform.position;
		projectile.rigidbody2D.velocity = Vector2.ClampMagnitude(new Vector2(ttp.x - tp.x, ttp.y - tp.y), projectileSpeed);
	}
}
