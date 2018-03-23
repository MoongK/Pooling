using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlWidth : MonoBehaviour {

    Text myWidth;
    float myPreferredWidth;
    public float limitter;

	void Start () {
        limitter = 100f;
        myWidth = GetComponent<Text>();
	}
	
	void Update ()
    {
        if (myWidth.preferredWidth >= limitter)
            GetComponent<LayoutElement>().preferredWidth = limitter;
        else
            GetComponent<LayoutElement>().preferredWidth = GetComponent<Text>().preferredWidth;
    }

}
