using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	// General well being
	public Statistic health;
	public Statistic happiness;

	// Relationship with PC
	public Statistic social;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Update state
	public void UpdateStats(float timeIncrement)
	{
		health.Update(timeIncrement);
		happiness.Update(timeIncrement);
		social.Update(timeIncrement);
	}
}
