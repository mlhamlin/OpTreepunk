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
        AIFlyingController flyScript = enemy.GetComponent<AIFlyingController>();
        if (flyScript != null)
        {
            flyScript.target = PlayerNode;
        }
        AIRangedController rangeScript = enemy.GetComponent<AIRangedController>();
        if (rangeScript != null)
        {
            rangeScript.trackingTarget = RaycastTarget;
            enemy.GetComponent<SpawnProjectile>().target = RaycastTarget;
        }

		currentEnemy = enemy.GetComponent<HealthData>();
		
		SpawnCount--;
	}
}
