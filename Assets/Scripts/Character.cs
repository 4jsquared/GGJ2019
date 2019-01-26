using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {
	// General well being
	public Statistic health;
	public Statistic happiness;
    
	// Relationship with PC
	public Statistic social;

    //Thresholds
    public float healthThreshHoldValue;
    public float happinessThreasholdValue;
    public float socialThresholdValue;


    //Threshold objects
    Threshold healthThreshold = new Threshold("health");
    Threshold happinessThreshold = new Threshold("happiness");
    Threshold socialThreshold = new Threshold("social");
    Threshold emptyThreshold = new Threshold("empty");


    Threshold lowestThreshold;

    Threshold[] thresholdList = new Threshold[3];

    //Emote bubble that appears above characters head
    public Button emoteBubble;

    //Action buttons
    public Button healthBubble;
    public Button happinessBubble;
    public Button socialBubble;

    //Icons for actionButtons
    public Sprite[] actionStatisticsIcons;
    
    //Icons for emoteButton
    public Sprite[] emoteStatisticsIcons;

    // Use this for initialization
    void Start () {
        emoteBubble.gameObject.SetActive(false);
        thresholdList[0] = healthThreshold;
        thresholdList[1] = happinessThreshold;
        thresholdList[2] = socialThreshold;
    }

    // Update is called once per frame
    void Update () {
        lowestThreshold = emptyThreshold;
        CheckHealthThreshHold();
        CheckHappinessThreshHold();
        CheckSocialThreshHold();
        FindLowestThreshold();
        DisplayIconOfLowestStatistics();
	}

    
    void DisplayIconOfLowestStatistics()
    {
        if (lowestThreshold.name == "health")
        {
            emoteBubble.image.sprite = emoteStatisticsIcons[0];
            emoteBubble.gameObject.SetActive(true);

        }
        else if (lowestThreshold.name == "happiness")
        {
            emoteBubble.image.sprite = emoteStatisticsIcons[2];
            emoteBubble.gameObject.SetActive(true);

        }
        else if (lowestThreshold.name == "social")
        {
            emoteBubble.image.sprite = emoteStatisticsIcons[1];
            emoteBubble.gameObject.SetActive(true);
        }
        else
        {
            emoteBubble.gameObject.SetActive(false);
        }
    }

    private void FindLowestThreshold()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("Threshhold in loop:  " + thresholdList[i].name + " value is " + thresholdList[i].value + " past threshold " + thresholdList[i].pastThreshold);

            if (thresholdList[i].pastThreshold == true && thresholdList[i].value > lowestThreshold.value)
            {
                lowestThreshold = thresholdList[i];
                //spareThreshold = thresholdList[i];                //Debug.Log("Lowest threshold name is after:  " + lowestThreshold.name);
                //Debug.Log("Lowest threshold value is after: " + lowestThreshold.value);
            } 
        }
    }

    void CheckHealthThreshHold()
    {
        if (health.Value < healthThreshHoldValue)
        {
            healthThreshold.pastThreshold = true;
            healthThreshold.value = (healthThreshHoldValue - health.Value)/healthThreshHoldValue;
        }
        else
        {
            healthThreshold.pastThreshold = false;

        }
    }

    void CheckHappinessThreshHold()
    {

        if (happiness.Value < happinessThreasholdValue)
        {
            happinessThreshold.pastThreshold = true;
            happinessThreshold.value = (happinessThreasholdValue - happiness.Value)/happinessThreasholdValue;
        }
        else
        {
            happinessThreshold.pastThreshold = false;
        }
    }

    void CheckSocialThreshHold()
    {
        if (social.Value < socialThresholdValue)
        {
            socialThreshold.pastThreshold = true;
            socialThreshold.value = (socialThresholdValue - social.Value)/socialThresholdValue;
        }
        else
        {
            socialThreshold.pastThreshold = false;
        }
    }

    // Update state
    public void UpdateStats(float timeIncrement)
	{
		health.Update(timeIncrement);
		happiness.Update(timeIncrement);
		social.Update(timeIncrement);
	}
}
