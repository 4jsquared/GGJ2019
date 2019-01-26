using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StoryEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Can we start this event?
	public abstract bool IsTriggered();

	// Should we repeat this event, or is it one time only
	public abstract bool IsRepeat();

	// Start the event (called by the storyteller)
	public abstract void StartEvent();

	// Update the event (called once per frame by the storyteller)
	public abstract bool UpdateEvent();
}
