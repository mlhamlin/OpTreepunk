using UnityEngine;
using System.Collections;

public class VineWhip : Action {

	Activator activator;
    public Activator defaultActivator;
	public Character character;
    public VineAnimation animation;

    void Start()
    {
        activator = defaultActivator;
    }

	public override void performAction()
	{
		if  (activator.type == "lever" && ((Lever)activator).isLeft != character.FacingRight())
		{
			activator.Activate();
            animation.ShootVine(activator.transform.position);
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
        if (collider.gameObject.layer == 12)
        {
            activator = collider.GetComponent<Activator>();
        }
	}

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 12 && collider.GetComponent<Activator>() == activator )
        {
            activator = defaultActivator;
        }
    }
}