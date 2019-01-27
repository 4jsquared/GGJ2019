using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {

    public Sprite xmasSprite;
    public Sprite holiSprite;
    public Sprite bdaySprite;
    public Sprite normSprite;

    private SpriteRenderer mySprite;

    public bool xmasTime { private get; set; }
	public bool holiTime { private get; set; }
	public bool bdayTime { private get; set; }

	// Use this for initialization
	void Start ()
    {
       mySprite = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (holiTime == true)
        {
            mySprite.sprite = holiSprite;
        }
        else if (xmasTime == true)
        {
            mySprite.sprite = xmasSprite;
        }
        else if (bdayTime == true)
        {
            mySprite.sprite = bdaySprite;
		}
		else
		{
			mySprite.sprite = normSprite;
		}
	}
}
