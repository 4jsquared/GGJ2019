using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterEvent : TimedEvent
{
	public override void StartEvent()
	{
		base.StartEvent();

		// Switch character option
	}

	public override bool UpdateEvent()
	{
		bool isRunning = base.UpdateEvent();

		if (!isRunning)
		{
			// Switch back to normal sprite
		}

		return isRunning;
	}
}
