using UnityEngine;
using System.Collections;

public class HealthPack : MonoBehaviour {

	public LayerMask Layer;
	public int healAmount;

	public void OnTriggerEnter2D(Collider2D enter) {

		if (LayerInMask(Layer, enter.gameObject.layer))
		{
			GameObject go = enter.gameObject;
			if (go != null)
			{
				HealthData healthData = go.transform.parent.gameObject.GetComponent<HealthData>();
				if (healthData != null)
				{
					healthData.Heal(healAmount);
					Destroy(this.gameObject);
				}
			}
		}
	}

	private bool LayerInMask(LayerMask mask, int layer)
	{
		return mask.value == 1<<layer;
	}
}
