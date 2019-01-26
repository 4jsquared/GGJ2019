using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a general handy way to handle any stats.
/// </summary>
[System.Serializable]
public struct Statistic
{
	// Code side accesor
	public float Value { get { return value; } }

    // The actual value (this is not directly settable, use relevant methods)
    [SerializeField] private float value;

    // The bounds
    [SerializeField] private float minimum;
    [SerializeField] private float maximum;

    // The natural rate of change
    [SerializeField] private float changePerSecond;

    public void Update(float timeIncrement)
    {
		Increment(timeIncrement * changePerSecond);
    }

    public void Increment(float inValue)
    {
        value += inValue;
		value = Mathf.Clamp(value, minimum, maximum);
	}

	public void Set(float inValue)
	{
		value = inValue;
		value = Mathf.Clamp(value, minimum, maximum);
	}
}
