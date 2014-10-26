using UnityEngine;
using System.Collections;

public class WaveTrigger : MonoBehaviour {

    public GameObject player;
    public SpawnPoint[] spawnpoints;
    bool triggered = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == player)
        {
            if (!triggered)
            {
                foreach (SpawnPoint spawnpoint in spawnpoints)
                {
                    spawnpoint.active = true;
                }
            }
            triggered = true;
        }
    }
}
