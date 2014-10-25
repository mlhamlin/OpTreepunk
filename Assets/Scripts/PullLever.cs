﻿using UnityEngine;
using System.Collections;

public class PullLever : Action {

	public Activator activator;
    public Activator defaultActivator;

	void Start() 
	{
        activator = defaultActivator;
	}

	public override void performAction()
	{
        print("pulling lever!");
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
        if (collider.gameObject.layer == 12 && collider.GetComponent<Activator>().Equals(activator))
        {
            activator = defaultActivator;
        }
    }
}