using UnityEngine;
using System.Collections;

public class StaminaPack: MonoBehaviour {

	public LayerMask Layer;
	public int staminaAmount;

	public void OnTriggerEnter2D(Collider2D enter) {

		if (LayerInMask(Layer, enter.gameObject.layer))
		{
			GameObject go = enter.gameObject;
			if (go != null)
			{
				Character character = go.transform.parent.gameObject.GetComponent<Character>();
				if (character != null)
				{
					character.addStamina(staminaAmount);
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
