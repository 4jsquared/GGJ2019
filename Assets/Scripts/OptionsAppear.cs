using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsAppear : MonoBehaviour
{
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        panel.gameObject.SetActive(false);
    }

    public void showHidePanel()
    {
        if (panel.gameObject.active)
        {
            panel.gameObject.SetActive(false);
        } else
        {
            panel.gameObject.SetActive(true);
        }
    }
}
