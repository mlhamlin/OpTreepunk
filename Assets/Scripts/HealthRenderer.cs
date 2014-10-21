using UnityEngine;
using System.Collections;

public class HealthRenderer : MonoBehaviour {
	public HealthData health;
	public Character stamina;
	private Vector2 originalScale;

	// Use this for initialization
	void Start () {
		originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (health != null)
			transform.localScale = new Vector2(originalScale.x * health.health / health.maxhealth, originalScale.y);
		else
			transform.localScale = new Vector2(originalScale.x * stamina.currentStamina / stamina.MaxStamina, originalScale.y);
	}
}
