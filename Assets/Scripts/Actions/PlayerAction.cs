using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : StoryAction
{
	[SerializeField] private float time;
	[SerializeField] private float money;

	[SerializeField] private Room goToRoom;
	[SerializeField] private Room returnToRoom;

	[SerializeField] private float realTime = 1;

	private float remainingTime;

	public override void StartAction(Character inCharacter)
	{
		if (inCharacter != null)
			throw new InvalidOperationException("Tried to apply player action with character");

		remainingTime = realTime;
		state = State.kTransitionIn;

		// Move to target character's room
		player.GoTo(goToRoom, () =>
		{
			// When they get there, transistion to acting state
			state = State.kAct;
		});
	}

	public override void UpdateAction()
	{
		if (state == State.kAct)
		{
			float timeThisUpdate = Mathf.Clamp(Time.deltaTime, 0, remainingTime);
			float fraction = (timeThisUpdate / realTime);

			// Cost money
			player.money += money * fraction;

			// Increment time
			world.time += time * fraction;

			// Update visual time
			remainingTime -= timeThisUpdate;
			if (remainingTime < Mathf.Epsilon)
			{
				state = State.kTransitionOut;

				player.GoTo(returnToRoom, () =>
				{
					// Finish the aciton on return.
					state = State.kInactive;
				});
			}
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
