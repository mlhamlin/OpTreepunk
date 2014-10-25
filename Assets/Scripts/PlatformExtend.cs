using UnityEngine;
using System.Collections;

public class PlatformExtend : Listener {

    Vector2 extendloc;
    Vector2 nonExtendLoc;
    Vector2 to, from;
    public bool isExtended;
    float t = 1;

	// Use this for initialization
	void Start () {
        if (isExtended)
        {
            nonExtendLoc = transform.position;
            extendloc = transform.position;
            extendloc.x -= 4;
            to = nonExtendLoc;
            from = extendloc;
            gameObject.transform.localPosition = extendloc;
        }
        else 
        {
            to = nonExtendLoc;
            from = extendloc;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (t < 1)
        {
            t += 0.02f;
        }
        gameObject.transform.localPosition = Vector2.Lerp(from, to, t);
	}

    public override void notify()
    {
        Vector2 lastFrom = from;
        from = to;
        to = lastFrom;
        t = 1-t;
        isExtended = !isExtended;
    }
}
