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

	public abstract bool IsAvailable();
	public abstract void DoAction();
}
