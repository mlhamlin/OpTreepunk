using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	public GameObject Enemy;
	public AINode PlayerNode;
	public GameObject RaycastTarget;
    public bool active;
	public bool InfiniteSpawn;
	public int SpawnCount;

	private HealthData currentEnemy;

	// Use this for initialization
	void Start () {
		Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		if (active && (InfiniteSpawn || (SpawnCount > 0)) && ((currentEnemy == null) || (currentEnemy.isDead())))
		{
			Spawn();
		}
	}

	public void Spawn()
	{
		GameObject enemy = (GameObject) Instantiate(Enemy, gameObject.transform.position, Quaternion.identity);
		AICharController aiScript = enemy.GetComponent<AICharController>();
		if (aiScript != null)
		{
			aiScript.nextNode = PlayerNode;
			aiScript.target = PlayerNode;
			aiScript.raycastTarget = RaycastTarget;
		}

		currentEnemy = enemy.GetComponent<HealthData>();
		
		SpawnCount--;
	}
}
