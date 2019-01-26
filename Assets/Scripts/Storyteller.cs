using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storyteller : MonoBehaviour
{
	// World
	public World world;
	public Player player;
	public Character[] characters;


	// Events
	public List<StoryEvent> onTriggerEvents;
	public List<StoryEvent> randomEvents;

	public float randomEventInterval;

	private List<StoryEvent> runningTriggeredEvents;
	private StoryEvent runningRandomEvent;

	private float accumulatedRandomEventTime;


	// Actions
	public List<StoryAction> actions;


	// Use this for initialization
	void Start ()
	{
		runningTriggeredEvents = new List<StoryEvent>();

		// Initialise actions
		foreach (StoryAction action in actions)
			action.Initialise(world, player);
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Update the stats of every object
		foreach (Character c in characters)
			c.UpdateStats();

		// Update any in flight events
		for (int i = 0; i < runningTriggeredEvents.Count; )
		{
			StoryEvent e = runningTriggeredEvents[i];
			if (!e.UpdateEvent())
			{
				// The event has finished, remove it from the running event list.
				runningTriggeredEvents.RemoveAt(i);

				// If it can be run again, add it back to the onTriggered list
				if (e.IsRepeat())
					onTriggerEvents.Add(e);
			}
			else
			{
				i++;
			}
		}

		// Ditto for random event
		if (runningRandomEvent != null)
		{
			if (!runningRandomEvent.UpdateEvent())
			{
				if (runningRandomEvent.IsRepeat())
				{
					randomEvents.Add(runningRandomEvent);
					runningRandomEvent = null;
				}
			}
		}

		// Check for any triggered events.
		// Do this before checking random events, because onTrigger events have higher priority
		for (int i = onTriggerEvents.Count - 1; i >= 0; i--)
		{
			StoryEvent e = onTriggerEvents[i];
			if (e.IsTriggered())
			{
				e.StartEvent();
				onTriggerEvents.RemoveAt(i);
				runningTriggeredEvents.Add(e);
			}
		}

		// Check for any random events
		if (runningRandomEvent == null && randomEvents.Count > 0)
		{
			// Don't accumulate time while an event is running - we don't want to keep triggering these.
			accumulatedRandomEventTime += Time.deltaTime;
			if (accumulatedRandomEventTime > randomEventInterval)
			{
				accumulatedRandomEventTime -= randomEventInterval;

				// Grab a random event
				int index = Random.Range(0, randomEvents.Count);
				runningRandomEvent = randomEvents[index];
				randomEvents.RemoveAt(index);

				// And start it
				runningRandomEvent.StartEvent();
			}
		}
	}
}
