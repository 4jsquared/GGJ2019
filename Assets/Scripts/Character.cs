using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	// General well being
	[SerializeField] private Statistic health;
	[SerializeField] private Statistic happiness;

	// Relationship with PC
	[SerializeField] private Statistic social;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Update state
	public void UpdateStats()
	{
		health.Update();
		happiness.Update();
		social.Update();
	}
}
