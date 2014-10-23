using UnityEngine;
using System.Collections;

public class VineAnimation : MonoBehaviour {

    float disableTime = 0;
    float numberToCreate = 0;
    float timeBetweenSegments = 0;
    float timeToNextSegment = 0;
    float vineWidth = 0.1f;
    public float vineSpeed = 1;
    public VineSegment segment;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (numberToCreate > 0)
        {
            timeToNextSegment -= Time.deltaTime;
            if (timeToNextSegment <= 0)
            {
                timeToNextSegment = timeBetweenSegments;
                VineSegment newseg = ((GameObject)Instantiate(segment.gameObject, transform.position, transform.rotation)).GetComponent<VineSegment>();
                if (transform.lossyScale.x < 0)
                {
                    newseg.speed = -vineSpeed;
                    newseg.setSpeed(-vineSpeed);
                }
                else
                {
                    newseg.speed = vineSpeed;
                    newseg.setSpeed(vineSpeed);
                }
                newseg.lifetime = numberToCreate * timeBetweenSegments;
                newseg.initialLifetime = newseg.lifetime;
                numberToCreate--;
            }
        }
        else if (disableTime > 0)
        {
            disableTime -= Time.deltaTime;
            if (disableTime <= 0)
            {
                transform.parent.parent.parent.GetComponent<Character>().enabled = true;
            }
        }
	}

    public void ShootVine(float dist)
    {
        numberToCreate = dist / vineWidth;
        timeBetweenSegments = vineWidth / vineSpeed;
        disableTime = dist / vineSpeed;
        print(transform.parent.parent);
        transform.parent.parent.parent.GetComponent<Character>().enabled = false;
    }
}
