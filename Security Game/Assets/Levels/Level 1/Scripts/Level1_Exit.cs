using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level1_Exit : MonoBehaviour
{
	public InputField passwordField;
	public GameObject openDoor;
	public GameObject chatNotice;
	public GameObject badgeScreen;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
		string input = passwordField.text;

		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (input == "thebestpassword")
			{
				openDoor.SetActive (true);
				chatNotice.SetActive (true);
			}
			passwordField.text = "";
			passwordField.ActivateInputField ();
		}

		if (openDoor.activeSelf && Input.GetMouseButtonDown (0))
		{
			badgeScreen.SetActive (true);
			openDoor.SetActive (false);
		}

		if (badgeScreen.activeSelf && Input.GetMouseButtonDown (0))
		{
			// Load cutscene/transition/intro to Level 2
		}
	}
}
