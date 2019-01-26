using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCharacterAction : StoryAction
{
	[SerializeField] private Character character;

	[SerializeField] private float health;
	[SerializeField] private float happiness;
	[SerializeField] private float social;

	[SerializeField] private float time;

	[SerializeField] private float money;

	public override void DoAction()
	{
		// Do character effects
		character.health.Increment(health);
		character.happiness.Increment(happiness);
		character.social.Increment(social);

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
		return inCharacter == character;
	}
}
