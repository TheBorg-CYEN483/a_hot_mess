using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level1_Exit : MonoBehaviour
{
	private int page;
	private bool buttonNext;

	public InputField passwordField;
	public GameObject openDoor;
	public GameObject chatNotice;
	public GameObject badgeScreen;

	// Use this for initialization
	void Start ()
	{
		page = 0;
		buttonNext = false;
		chatNotice.SetActive (true);
		chatNotice.GetComponent<Text> ().text = "\"thebestpassword\" must be their password! Go type it in!";
		openDoor.SetActive (false);
		badgeScreen.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		string input = passwordField.text;

		if (badgeScreen.activeSelf && page == 2 && buttonNext)
		{
			buttonNext = false;
			PlayerPrefs.SetString (PlayerPrefs.GetString ("Username") + "." + PlayerPrefs.GetString ("Password") + ".Level1", "Level 2_Scene");
			SceneManager.LoadScene ("Level 2_Scene");
//			return;
		}

		if (openDoor.activeSelf && page == 1 && buttonNext)
		{
			buttonNext = false;
			badgeScreen.SetActive (true);
			openDoor.SetActive (false);
			page++;
		}

		if (page == 0 && (Input.GetKeyDown(KeyCode.Return) || buttonNext))
		{
			buttonNext = false;
			if (input == "thebestpassword")
			{
				openDoor.SetActive (true);
				chatNotice.GetComponent<Text> ().text = "Now that you're on the network, the door unlocked to let you through! " +
				"Keep going, and be careful! Who knows what might happen to you if they find out you hacked in!";
				page++;
			}
			passwordField.text = "";
		}
		passwordField.ActivateInputField ();
	}

	public void flagAdvance()
	{
		buttonNext = true;
	}
}
