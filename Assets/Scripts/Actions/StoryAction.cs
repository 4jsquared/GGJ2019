using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StoryAction : MonoBehaviour
{
	[SerializeField] private string description;

	protected World world;
	protected Player player;

	public void Initialise(World inWorld, Player inPlayer)
	{
		world = inWorld;
		player = inPlayer;
	}

	// How should we describe this action
	// TODO this should probably be edited to be most convenient for how the UI wants to work
	// TODO may need to be overriden for specific actions
	public string GetActionDescription()
	{
		return description;
	}

	// Does this action primarily affect this character.
	// This is used to determine whether to show this action in the list of character specific actions.
	public abstract bool IsPrimaryCharacter(Character inCharacter);
	// This action is primarily associated with the PC
	// This is used to determine actions for the general action list.
	public abstract bool IsPlayerAction();

	// Is the action currently available.
	public abstract bool IsAvailable();

	// Actually do the action - pass in the selected character if need be.
	public abstract void DoAction(Character inCharacter);
}
