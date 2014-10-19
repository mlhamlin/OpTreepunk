using UnityEngine;
using System.Collections;

public class PullLever : Action {

	Activator activator;
    public Activator defaultActivator;
	Character character;

	void Start() 
	{
		character = gameObject.GetComponent<Character> ();
	}

	public override void performAction()
	{
			activator.Activate();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
        if (collider.gameObject.layer == 12)
        {
            activator = collider.GetComponent<Activator>();
        }
	}

    void OnTriggerLeave2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 12 && collider.GetComponent<Activator>() == activator )
        {
            activator = defaultActivator;
        }
    }
}