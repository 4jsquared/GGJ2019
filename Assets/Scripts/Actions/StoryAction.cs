using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StoryAction : MonoBehaviour
{
	protected World world;
	protected Player player;

	public void Initialise(World inWorld, Player inPlayer)
	{
		world = inWorld;
		player = inPlayer;
	}

	// Does this action primarily affect this character.
	// This is used to determine whether to show this action in the list of character specific actions.
	public abstract bool IsPrimaryCharacter(Character inCharacter);
	public abstract bool IsAvailable();
	public abstract void DoAction();
}
