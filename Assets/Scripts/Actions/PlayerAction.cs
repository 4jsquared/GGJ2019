using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : StoryAction
{
	[SerializeField] private float time;
	[SerializeField] private float money;

	[SerializeField] private Room room;

	public override void StartAction(Character inCharacter)
	{
		if (inCharacter != null)
			throw new InvalidOperationException("Tried to apply player action with character");

		state = State.kAct;
	}

	public override void UpdateAction()
	{
		if (false)
		{
			// Cost money
			player.money -= money;

			// Increment time
			world.time += time;
		}
	}

	public override bool IsAvailable()
	{
		// If this increases money, or doesn't bring us to negative, allow it.
		return money < 0 || player.money >= money;
	}

	public override bool IsPlayerAction()
	{
		return true;
	}

	public override bool IsPrimaryCharacter(Character inCharacter)
	{
		// Only affects the player
		return false;
	}
}
