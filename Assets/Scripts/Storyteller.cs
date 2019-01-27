﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
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


	// Actions
	public List<StoryAction> actions;


	// Are interactions available
	public bool IsInteractive { get; private set; }


	// Internal tracking
	private float elapsedTime;

	private List<StoryEvent> runningTriggeredEvents;
	private StoryEvent runningRandomEvent;
	private StoryAction runningAction;

	private float accumulatedRandomEventTime;


	// Use this for initialization
	void Start ()
	{
		runningTriggeredEvents = new List<StoryEvent>();

		// Initialise actions
		foreach (StoryAction action in actions)
			action.Initialise(this, world, player);

		// Intialise characters
		foreach (Character c in characters)
			c.Initialise(this, actions.Where(action => action.IsPrimaryCharacter(c)));

		// Initialise player
		player.Initialise(world);

		IsInteractive = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Calculate elapsed time since last update
		float timeIncrement = world.time - elapsedTime;
		elapsedTime = world.time;

		// Update the stats of every object
		foreach (Character c in characters)
			c.UpdateStats(timeIncrement);

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
			accumulatedRandomEventTime += timeIncrement;
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

		// Update the action state
		if (runningAction != null)
		{
			runningAction.UpdateAction();
			switch (runningAction.GetState())
			{
				case StoryAction.State.kInactive:
					// Finished, remove
					runningAction = null;
					IsInteractive = true;
					break;
				case StoryAction.State.kTransitionIn:
					// Starting, but needs transition period
					break;
				case StoryAction.State.kAct:
					// Acting
					// TODO adjust time rate
					break;
				case StoryAction.State.kTransitionOut:
					// Action finished, returning to normal
					// TODO adjust time rate back
					break;
			}
		}
	}


	public void TriggerAction(StoryAction inAction, Character inCharacter)
	{
		if (runningAction == null)
		{
			runningAction = inAction;
			runningAction.StartAction(inCharacter);
			IsInteractive = false;
		}
	}
}
