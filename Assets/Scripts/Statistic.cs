using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a general handy way to handle any stats.
/// </summary>
[System.Serializable]
public struct Statistic
{
    // The actual value
    public float value;

    // The bounds
    [SerializeField] private float minimum;
    [SerializeField] private float maximum;

    // The natural rate of change
    [SerializeField] private float changePerSecond;

    public void Update()
    {
        value += changePerSecond * Time.deltaTime;
        value = Mathf.Clamp(value, minimum, maximum);
    }

    public void IncrementValue(float value)
    {
        value += value;
    }
}
