using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// attach this to Scroll View "CrackWindow"
public class CrackScroller : MonoBehaviour 
{
	private ScrollRect sr;

	// Use this for initialization
	void Start () 
	{ 
		sr = this.GetComponent<ScrollRect>(); 
		sr.verticalNormalizedPosition = 1f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (sr.verticalNormalizedPosition > 0.29)
			sr.verticalNormalizedPosition -= 0.005f;
	}
}
