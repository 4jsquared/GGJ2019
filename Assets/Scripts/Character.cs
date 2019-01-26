using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character : MonoBehaviour
{
	// General well being
	public Statistic health;
	public Statistic happiness;

	// Relationship with PC
	public Statistic social;

	// Actions
	private IEnumerable<StoryAction> characterActions;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Initialise(IEnumerable<StoryAction> inActions)
	{
		characterActions = inActions;
	}

	// List of available actions. This is auto populated
	public IEnumerable<StoryAction> GetAvailableActions()
	{
		return characterActions.Where(c => c.IsAvailable());
	}

	// Update state
	public void UpdateStats(float timeIncrement)
	{
		health.Update(timeIncrement);
		happiness.Update(timeIncrement);
		social.Update(timeIncrement);
	}
}
