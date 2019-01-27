using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
	public enum ActionSprites
	{
		kHealth,
		kHappiness,
		kSocial,
	};

	public Room room;

	// General well being
	public Statistic health;
	public Statistic happiness;
    
	// Relationship with PC
	public Statistic social;

    //Thresholds
    [SerializeField] private float healthThreshHoldValue;
	[SerializeField] private float happinessThreasholdValue;
	[SerializeField] private float socialThresholdValue;

	// Sprites for actions
	[SerializeField] private Sprite healthBubble;
	[SerializeField] private Sprite happinessBubble;
	[SerializeField] private Sprite socialBubble;

	// Sprites for emote
	[SerializeField] private Sprite healthEmote;
	[SerializeField] private Sprite happinessEmote;
	[SerializeField] private Sprite socialEmote;

	[SerializeField] private float spriteScale;
	[SerializeField] private float spriteOffset;

	[SerializeField] private bool showActions;


	// Actions
	private IEnumerable<StoryAction> characterActions;


    // Threshold objects
    Threshold healthThreshold = new Threshold("health");
    Threshold happinessThreshold = new Threshold("happiness");
    Threshold socialThreshold = new Threshold("social");
    Threshold emptyThreshold = new Threshold("empty");

    Threshold lowestThreshold;

    Threshold[] thresholdList = new Threshold[3];

	// Sprite cloud renderers
	private SpriteRenderer emoteSprite;
	private GameObject actionSpriteRoot;
	private List<OptionsAction> actionSpriteCloud;



	// Use this for initialization
	void Start ()
	{
        thresholdList[0] = healthThreshold;
        thresholdList[1] = happinessThreshold;
        thresholdList[2] = socialThreshold;

		// Create a new sprite renderer for the emote
		GameObject emoteObject = new GameObject(name + " Emote");
		emoteObject.AddComponent<SpriteRenderer>();
		emoteObject.transform.parent = transform; // Relative to this object
		emoteObject.transform.localPosition = new Vector2(0, spriteOffset); // Positioned above
		emoteObject.transform.localScale = new Vector2(spriteScale, spriteScale);

		emoteSprite = emoteObject.GetComponent<SpriteRenderer>();
		emoteSprite.enabled = false; // Initially disabled
		emoteSprite.sortingLayerName = "WorldUI"; // Make sure this renders in the UI layer
		
		// Create action sprite cloud
		actionSpriteRoot = new GameObject(name + " Actions");
		actionSpriteRoot.transform.parent = transform; // Relative to this object
		actionSpriteRoot.transform.localPosition = new Vector2(0, 0);
		actionSpriteRoot.SetActive(false);

		actionSpriteCloud = new List<OptionsAction>();
	}

	public void Initialise(IEnumerable<StoryAction> inActions)
	{
		characterActions = inActions;
	}

	private void OnMouseDown()
	{
		showActions = !showActions;
	}

	public void HideActions()
	{
		showActions = false;
	}

	// Update is called once per frame
	void Update()
	{
		// Calculate emote
        lowestThreshold = emptyThreshold;
        CheckHealthThreshHold();
        CheckHappinessThreshHold();
        CheckSocialThreshHold();
        FindLowestThreshold();
        DisplayIconOfLowestStatistics();

		// Calculate actions
		if (showActions)
		{
			StoryAction[] actions = GetAvailableActions().ToArray();

			if (actions.Count() != actionSpriteCloud.Count)
			{
				// Recalculate and position renderers

				// Get the correct number of sprites
				while (actions.Count() > actionSpriteCloud.Count)
					actionSpriteCloud.Add(CreateActionSprite());
				if (actions.Count() < actionSpriteCloud.Count)
					actionSpriteCloud.RemoveRange(actions.Count(), actionSpriteCloud.Count - actions.Count());

				// Calculate position
				int numPositions = actionSpriteCloud.Count + 1; // Don't forget emote buble space
				float anglePerObject = Mathf.PI * 2 / numPositions;

				for (int i = 0; i < actionSpriteCloud.Count; i++)
				{
					actionSpriteCloud[i].transform.localPosition = spriteOffset * new Vector2(
						Mathf.Sin((i + 1) * anglePerObject),
						Mathf.Cos((i + 1) * anglePerObject)
					);
				}
			}

			// Set the correct sprite and click event
			for (int i = 0; i < actionSpriteCloud.Count; i++)
			{
				actionSpriteCloud[i].GetComponent<SpriteRenderer>().sprite = GetSpriteForAction(actions[i].GetActionDescription());
				actionSpriteCloud[i].action = actions[i];
			}

			// Activate
			actionSpriteRoot.SetActive(true);
		}
		else
		{
			actionSpriteRoot.SetActive(false);
		}
    }

	private OptionsAction CreateActionSprite()
	{
		GameObject actionObject = new GameObject(name + " Action");

		actionObject.AddComponent<SpriteRenderer>();
		actionObject.AddComponent<CircleCollider2D>();
		actionObject.AddComponent<OptionsAction>();
		
		actionObject.transform.parent = actionSpriteRoot.transform; // Relative to root
		actionObject.transform.localScale = new Vector2(spriteScale, spriteScale);
		actionObject.GetComponent<SpriteRenderer>().sortingLayerName = "WorldUI"; // Make sure this renders in the UI layer

		CircleCollider2D collider = actionObject.GetComponent<CircleCollider2D>();
		collider.isTrigger = true;
		collider.radius = spriteScale;
		
		OptionsAction optionsAction = actionObject.GetComponent<OptionsAction>();
		optionsAction.character = this;

		return optionsAction;
	}

	private Sprite GetSpriteForAction(ActionSprites actionSprite)
	{
		switch (actionSprite)
		{
			case ActionSprites.kHappiness:
				return happinessBubble;
			case ActionSprites.kHealth:
				return healthBubble;
			case ActionSprites.kSocial:
				return socialBubble;
			default:
				throw new InvalidOperationException("Invalid action sprite");
		}
	}

	// List of available actions. This is auto populated
	public IEnumerable<StoryAction> GetAvailableActions()
	{
		return characterActions.Where(c => c.IsAvailable());
	}

    
    void DisplayIconOfLowestStatistics()
    {
        if (lowestThreshold.name == "health")
        {
            emoteSprite.sprite = healthEmote;
            emoteSprite.enabled = true;
        }
        else if (lowestThreshold.name == "happiness")
        {
            emoteSprite.sprite = happinessEmote;
            emoteSprite.enabled = true;

        }
        else if (lowestThreshold.name == "social")
        {
            emoteSprite.sprite = socialEmote;
            emoteSprite.enabled = true;
        }
        else
        {
            emoteSprite.enabled = false;
        }
    }

    private void FindLowestThreshold()
    {
        for (int i = 0; i < 3; i++)
        {
            // Debug.Log("Threshhold in loop:  " + thresholdList[i].name + " value is " + thresholdList[i].value + " past threshold " + thresholdList[i].pastThreshold);

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
