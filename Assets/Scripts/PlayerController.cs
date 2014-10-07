using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public KeyCode leftkey;
	public KeyCode rightkey;
	public KeyCode upkey;
	public int jumpspeed;
	public int movespeed;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(upkey))
			rigidbody2D.velocity = new Vector2(0, jumpspeed);
		if (Input.GetKey(leftkey))
			rigidbody2D.velocity = new Vector2(-movespeed, rigidbody2D.velocity.y);
		if (Input.GetKey(rightkey))
			rigidbody2D.velocity = new Vector2(movespeed, rigidbody2D.velocity.y);
	}
}
