using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level3achievment : MonoBehaviour {

	// Use this for initialization
	void Start () 
	
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
       System.Threading.Thread.Sleep(2000);
	   SceneManager.LoadScene(("GoToLevel4"));
	}
}
