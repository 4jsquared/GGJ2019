using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StoryAction : MonoBehaviour
{
	public enum State
	{
		kInactive,
		kTransitionIn,
		kAct,
		kTransitionOut
	}

	[SerializeField] private Character.ActionSprites description;

	protected State state;

	protected World world;
	protected Player player;
	private Storyteller storyteller;

	public void Start()
	{
		state = State.kInactive;
	}

	public void Initialise(Storyteller inStoryteller, World inWorld, Player inPlayer)
	{
		world = inWorld;
		player = inPlayer;
		storyteller = inStoryteller;
	}

	// How should we describe this action
	public Character.ActionSprites GetActionDescription()
	{
		return description;
	}

	public State GetState()
	{
		return state;
	}

	public void Trigger(Character inCharacter)
	{
		storyteller.TriggerAction(this, inCharacter);
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
	public abstract void StartAction(Character inCharacter);
	public abstract void UpdateAction();
}
