using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsAction : MonoBehaviour
{
	public Character character;
	public StoryAction action;

	private void OnMouseDown()
	{
		character.HideActions();
	}
}
