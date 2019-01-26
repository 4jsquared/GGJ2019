using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volumer : MonoBehaviour
{

    public AudioSource whistleAudio;
    public AudioSource guitarAudio;
    public AudioSource backingAudio;
    public AudioSource sadAudio;

    // Use this for initialization
    void Start ()
    {
        AudioSource whistleAudio = GetComponent<AudioSource>();
        AudioSource guitarAudio = GetComponent<AudioSource>();
        AudioSource backingAudio = GetComponent<AudioSource>();
        AudioSource sadAudio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
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
        if (guitarAudio.volume <= 0f)
        {
            backingAudio.volume = backingAudio.volume - .1f;
        }
        if (backingAudio.volume <= 0f)
        {
            sadAudio.volume = sadAudio.volume + .1f;
        }

    }
}
