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

	public override void DoAction(Character inCharacter)
	{
		if (!applicableCharacters.Contains(inCharacter))
			throw new InvalidOperationException("Tried to apply action with invalid selected character");

		// Do character effects
		inCharacter.health.Increment(health);
		inCharacter.happiness.Increment(happiness);
		inCharacter.social.Increment(social);

		// Cost money
		player.money -= money;

		// Increment time
		world.time += time;
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
