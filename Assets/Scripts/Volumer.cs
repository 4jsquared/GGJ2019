using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volumer : MonoBehaviour
{

	public AudioSource[] monitorSources;
	public AudioSource backgroundAudio;
	public AudioSource playerAudio;
	public AudioSource sadAudio;

	
	// Update is called once per frame
	void Update ()
    {
		/*
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            whistleAudio.volume = whistleAudio.volume - .1f;
            Debug.Log("Shifty");

            if (whistleAudio.volume <= 0f)
            {
                Debug.Log("NoWhistle"); 
                whistleAudio.volume = 0f;
                guitarAudio.volume = guitarAudio.volume - .1f;
            }
        }
		*/
		int numSilentSources = 0;
		foreach (AudioSource source in monitorSources)
		{
			if (source.volume <= Mathf.Epsilon)
				numSilentSources++;
		}

		float backgroundTarget = ((float)(monitorSources.Length - numSilentSources)) / monitorSources.Length;
		backgroundAudio.volume = Mathf.Clamp(backgroundTarget, backgroundAudio.volume - 0.1f, 1);

		if (backgroundTarget <= Mathf.Epsilon)
		{
			playerAudio.volume = Mathf.Clamp01(playerAudio.volume - .1f);
			sadAudio.volume = Mathf.Clamp01(sadAudio.volume + .1f);
        }

    }
}
