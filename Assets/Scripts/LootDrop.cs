using UnityEngine;
using System.Collections;

public class LootDrop : MonoBehaviour {

	public GameObject[] loot;
	public int chanceNothing;

	public void Drop () {
		int rnd = Random.Range (0, 100);
		if (rnd > chanceNothing) 
		{
			rnd = Random.Range(0, loot.Length);
			Instantiate(loot[rnd], gameObject.transform.position, Quaternion.identity);
		}
	}
}
