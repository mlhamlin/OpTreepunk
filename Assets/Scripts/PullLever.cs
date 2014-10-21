using UnityEngine;
using System.Collections;

public class PullLever : Action {

	Activator activator;
    public Activator defaultActivator;

	void Start() 
	{
        activator = defaultActivator;
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

    void OnTriggerExit2D(Collider2D collider)
    {
        print("collder " + collider + " left!");
        if (collider.gameObject.layer == 12 && collider.GetComponent<Activator>().Equals(activator))
        {
            print("leaving switch " + activator);
            activator = defaultActivator;
        }
    }
}