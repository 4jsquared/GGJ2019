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

	private World world;
	private Player player;

	public override void DoAction()
	{
		// Do character effects
		character.health.IncrementValue(health);
		character.happiness.IncrementValue(happiness);
		character.social.IncrementValue(social);

		player.money -= money;

		// Increment time
		world.time += time;
	}

	public void Initialise(World world, Player player)
	{
		
	}

	public override bool IsAvailable()
	{
		// TODO Check other factors for availablity
		return player.money >= money;
	}
}
