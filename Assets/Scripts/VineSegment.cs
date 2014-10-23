using UnityEngine;
using System.Collections;

public class VineSegment : MonoBehaviour {

    public float speed = 0;
    public bool extending = true;
    public float lifetime = 0;
    public float initialLifetime = 0;

	// Use this for initialization
	void Start () {
	
	}
	
    public void setSpeed(float speed)
    {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
    }

	// Update is called once per frame
	void Update () {
	    if (lifetime <= 0)
        {
            if (extending)
            {
                extending = false;
                lifetime = initialLifetime;
                setSpeed(-speed);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        lifetime -= Time.deltaTime;
	}
}
