using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsAction : MonoBehaviour
{
	public Character character;
	public StoryAction action;
	public Player player;

	private void OnMouseDown()
	{
		action.Trigger(character);

		if (character)
			character.HideActions();
		if (player)
			player.HideActions();
	}
}
