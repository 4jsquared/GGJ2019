using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsAction : MonoBehaviour
{
    public Character character1;
    // Start is called before the first frame update
    public int value;

    public GameObject panel;

    //public Button button;

    void Start()
    {
        //button.onClick.AddListener(IncreaseCharacterFood);
    }

    // Update is called once per frame
    public void IncreaseCharacterHappiness()
    {
        character1.happiness.Increment(value);
        panel.gameObject.SetActive(false);
    }

    public void IncreaseCharacterSocial()
    {
        character1.social.Increment(value);
        panel.gameObject.SetActive(false);
    }

    public void IncreaseCharacterFood()
    {
        character1.health.Increment(value);
        panel.gameObject.SetActive(false);
    }
}
