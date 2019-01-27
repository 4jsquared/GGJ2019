using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SingleCharacterAction : StoryAction
{
	[SerializeField] private Character[] applicableCharacters;

	[SerializeField] private float health;
	[SerializeField] private float happiness;
	[SerializeField] private float social;

	[SerializeField] private float time = 1;
	[SerializeField] private float realTime = 1;

	[SerializeField] private float money;

	private Character selectedCharacter;
	private float remainingTime;

	public override void StartAction(Character inCharacter)
	{
		if (!applicableCharacters.Contains(inCharacter))
			throw new InvalidOperationException("Tried to apply action with invalid selected character");

		selectedCharacter = inCharacter;
		remainingTime = realTime;
		state = State.kTransitionIn;

		// Move to target character's room
		player.GoTo(selectedCharacter.room, () =>
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

			// Do character effects
			selectedCharacter.health.Increment(health * fraction);
			selectedCharacter.happiness.Increment(happiness * fraction);
			selectedCharacter.social.Increment(social * fraction);

			// Cost money
			player.money -= money * fraction;

			// Increment time
			world.time += time * fraction;

			// Update visual time
			remainingTime -= timeThisUpdate;
			if (remainingTime < Mathf.Epsilon)
			{
				state = State.kInactive;

				// Tidy up and end
				selectedCharacter = null;
				state = State.kInactive;
			}
		}
	}

	public override bool IsAvailable()
	{
		// TODO Check other factors for availablity
		return player.money >= money;
	}

	public override bool IsPlayerAction()
	{
		return false;
	}

	public override bool IsPrimaryCharacter(Character inCharacter)
	{
		return applicableCharacters.Contains(inCharacter); ;
	}
}
