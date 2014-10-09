using UnityEngine;
using System.Collections;

public class HealthRenderer : MonoBehaviour {
	public HealthData health;
	private Vector2 originalScale;

	// Use this for initialization
	void Start () {
		originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector2(originalScale.x * health.health / health.maxhealth, originalScale.y);
	}
}
