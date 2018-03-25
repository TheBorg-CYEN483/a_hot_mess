using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StairsFromLevel3 : MonoBehaviour {

// Use this for initialization
	void Start () 
	
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	   SceneManager.LoadScene(("Level 4"));
	}
}
