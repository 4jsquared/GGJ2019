using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEvent : StoryEvent
{
	[SerializeField] private bool ShouldRepeat;
	[SerializeField] private float StartTime;
	[SerializeField] private float Duration;
	[SerializeField] private float RepeatAfterDuration;

	private float EndTime;

	public override bool IsRepeat()
	{
		return ShouldRepeat;
	}

	public override bool IsTriggered()
	{
		return Time.time > StartTime;
	}

	public override void StartEvent()
	{
		EndTime = Time.time + Duration;
	}

	public override bool UpdateEvent()
	{
		if (Time.time < EndTime)
		{
			// Event still in progress
			return true;
		}
		else
		{
			// Event ended, check if we need to reset the timer
			if (ShouldRepeat)
			{
				StartTime = Time.time + RepeatAfterDuration;
			}

			return false;
		}
	}

	protected void CancelEvent()
	{
		EndTime = 0;
	}
}
