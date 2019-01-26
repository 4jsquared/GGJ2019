using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockSpinner : MonoBehaviour {

    public float spinSpeed;
    private bool timeToMove = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (timeToMove == true)
        {
            transform.Rotate(0, 0, spinSpeed * -Time.deltaTime);
        }
    }
}
