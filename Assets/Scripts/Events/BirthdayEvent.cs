using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirthdayEvent : TimedEvent
{
	public Character character;
	public Character[] visualOnlyCharacters;

	[SerializeField] private float health;
	[SerializeField] private float happiness;
	[SerializeField] private float social;

	[SerializeField] private float time = 1;

	[SerializeField] private float money;

	public override void StartEvent()
	{
		base.StartEvent();

		if (player.money >= -money)
		{
			player.money += money;

			// Switch character art

			// Adjust character stats
			character.health.Increment(health);
			character.happiness.Increment(happiness);
			character.social.Increment(social);
		}
		else
		{
			CancelEvent();
		}
	}

	public override bool UpdateEvent()
	{
		bool isRunning = base.UpdateEvent();

		if (!isRunning)
		{
			// Switch back to normal sprite
		}

		return isRunning;
	}
}
