using UnityEngine;
using System.Collections;

public class KudzuCamera : MonoBehaviour {

	public GameObject follow;
	public Rect followrect;
	public Rect moverect;

	// Use this for initialization
	void Start () {
		float xadjust = (follow.transform.position.x - followrect.x) / followrect.width;
		float camx = moverect.x + xadjust * moverect.width;
		float yadjust = (follow.transform.position.y - followrect.y) / followrect.height;
		float camy = moverect.y + yadjust * moverect.height;
		gameObject.transform.position = new Vector3 (camx, camy, -1.0f);

	}
	
	// Update is called once per frame
	void Update () {
		float xadjust = (follow.transform.position.x - followrect.x) / followrect.width;
		float camx = moverect.x + xadjust*moverect.width;
		float yadjust = (follow.transform.position.y - followrect.y) / followrect.height;
		float camy = moverect.y + yadjust * moverect.height;
		gameObject.transform.position = new Vector3 (camx, camy, -1.0f);
	}
}
