using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1_Intro : MonoBehaviour
{
	public Button leftButton;
	public Text dialogue;
	public GameObject client;
	public GameObject router;
	public GameObject broadcast;
	public GameObject connectionText;
	private int page = 0;
	private bool activate = false;
	private Dictionary<int, Action> pageMethods;
	private List<string> openingDialogue = new List<string>() {
		"Ada: Okay, so you're in the tower now. Looks like you only have one door to go through. That should be easy, right?",
		"Charles: It's locked, though. This guy knows how to protect himself. I think you're going to have to get your computer on his network, " +
		   "so you can pretend you're him. Maybe then the door will unlock for you. Step one for breaking in: crack the password to his network.",
		"Charles: We're pretty sure the hardware he's using is the same as ours, so we know we can get in if his password is weak. We're going to " +
		    "need to do a dictionary attack. It's pretty sketchy, but this is for a good cause.",
		// Switch perspective from room to Handshake Description
		"Ada: You can think of wireless communication between computers like whales songs. A computer calls out to look for a router. " + 
		    "The router hears the computer and calls back to let it know it's here, too. Then they keep talking, connect, and become friends. It's adorable, really.",
		"", "", "", "", "", 
		// Last frame of Description
		"Ada: To start the attack, you're going to have to capture the first couple of messages at the start of a new connection.",
		"Charles: The router and computer will keep connecting even after we capture the messages. That's fine. We just need to get the information before they connect! " +
		    "After the router and computer establish their conection, it's like theiy're talking through a phone line. We can't catch the information they're sharing anymore.",
		"Ada: We also have a giant list of weak passwords. It's passwords that you would be able to find in the dictionary, like \"waffle\" or \"barnacle\"." +
		    "You're going to take the list and the messages we captured and compare them.",
		"Charles: We have a script to do that comparison for you! If the script finds a match , you'll know the password and be able to connect to his network!",
		// Scene change to Level
	};


	// Use this for initialization
	void Start()
	{
		pageMethods = new Dictionary<int, Action>() {
			{ 0, ShowPage_0 },
			{ 1, ShowPage_1 },
			{ 2, ShowPage_2 },
			// Handshake Transition
			{ 3, ShowPage_3 },
			{ 4, ShowPage_4 },
			{ 5, ShowPage_5 },
			{ 6, ShowPage_6 },
			{ 7, ShowPage_7 },
			{ 8, ShowPage_8 },

			{ 9, ShowPage_9 },
			{ 10, ShowPage_10 },
			{ 11, ShowPage_11 },
			{ 12, ShowPage_12 }

		};
	}

	private void ShowPage_12()
	{
		dialogue.text = openingDialogue[page];
	}

	private void ShowPage_11()
	{
		dialogue.text = openingDialogue[page];
	}

	private void ShowPage_10()
	{
		dialogue.text = openingDialogue[page];
	}

	private void ShowPage_9()
	{
		dialogue.text = openingDialogue[page];
	}

	private void ShowPage_8()
	{
		dialogue.text = openingDialogue[page];
	}

	private void ShowPage_7()
	{
		// dialogue.text = openingDialogue[page];
	}

	private void ShowPage_6()
	{
		// dialogue.text = openingDialogue[page];
	}

	private void ShowPage_5()
	{
		// dialogue.text = openingDialogue[page];
	}

	private void ShowPage_4()
	{
		dialogue.text = openingDialogue[page];
	}

	private void ShowPage_3()
	{
		dialogue.text = openingDialogue[page];
	}

	private void ShowPage_2()
	{
		dialogue.text = openingDialogue[page];
	}

	private void ShowPage_1()
	{
		leftButton.interactable = true;
		dialogue.text = openingDialogue[page];
	}

	private void ShowPage_0()
	{
		leftButton.interactable = false;
		dialogue.text = openingDialogue[page];
	}

	public void ClickNext()
	{
		if (page < (pageMethods.Count - 1))
		{
			page++;
		}
		else
		{
			SceneManager.LoadScene("Level 1");
		}
	}

	public void ClickPrev()
	{
		if (page > 0)
		{
			page--;
		}
	}

	void Update() { pageMethods[page].Invoke(); }

}
