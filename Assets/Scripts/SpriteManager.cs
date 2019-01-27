using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {

    public Sprite xmasSprite;
    public Sprite holiSprite;
    public Sprite bdaySprite;
    public Sprite normSprite;

    public int bdayDate;

    public SpriteRenderer mySprite;

    public bool xmasTime = false;
    private bool holiTime = false;
    private bool bdayTime = false;
    public bool normTime = true;

    // Use this for initialization
    void Start ()
    {
       // SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (normTime == true)
        {

            xmasTime = false;
            bdayTime = false;
            holiTime = false;
            mySprite.sprite = normSprite;
        }
        if (holiTime == true)
        {
            normTime = false;
            mySprite.sprite = holiSprite;
        }
        if (xmasTime == true)
        {
            normTime = false;
            mySprite.sprite = xmasSprite;

        }
        if (bdayTime == true)
        {
            normTime = false;
            mySprite.sprite = bdaySprite;
        }
    }
}
