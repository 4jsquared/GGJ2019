using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StoryEvent : MonoBehaviour
{
	protected Player player;
	protected World world;
	protected Storyteller storyteller;

	public void Initialise(Storyteller inStoryteller, World inWorld, Player inPlayer)
	{
		player = inPlayer;
		world = inWorld;
		storyteller = inStoryteller;
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
