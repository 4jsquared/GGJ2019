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

	[SerializeField] private float time;

	[SerializeField] private float money;

	private Character selectedCharacter;

	public override void StartAction(Character inCharacter)
	{
		if (!applicableCharacters.Contains(inCharacter))
			throw new InvalidOperationException("Tried to apply action with invalid selected character");

		selectedCharacter = inCharacter;
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
			// TODO span this out over the actions visual time

			// Do character effects
			selectedCharacter.health.Increment(health);
			selectedCharacter.happiness.Increment(happiness);
			selectedCharacter.social.Increment(social);

			// Cost money
			player.money -= money;

			// Increment time
			world.time += time;

			// Tidy up and end
			selectedCharacter = null;
			state = State.kInactive;
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
