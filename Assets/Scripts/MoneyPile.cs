using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPile : MonoBehaviour
{
	[SerializeField] private SpriteRenderer[] moneySprites;
	[SerializeField] private float moneyMax;

	[SerializeField] private Player player;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		int numSprites = (int)(moneySprites.Length * player.money / moneyMax);

		foreach (SpriteRenderer renderer in moneySprites)
		{
			if (numSprites > 0)
				renderer.enabled = true;
			else
				renderer.enabled = false;

			numSprites--;
		}
	}
}
