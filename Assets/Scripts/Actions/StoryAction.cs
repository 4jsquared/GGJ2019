using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StoryAction : MonoBehaviour
{
	public abstract bool IsAvailable();
	public abstract void DoAction();
}
