using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ProgressHandlerL1
{
	// Progress Tracking
	private int currScenePhase = 0;

	// Data Structs
	private List<string> manPanes  = new List<string> ();
	private List<string> chatPanes = new List<string> ();

	// Interactable Scene Objects
	private Scrollbar assist_scrollbar;
	private Text player_assistance_text;
	private Button advanceChat;
	private Button retractChat;
	private int currAssistPane = 0;

	// Dynamic Objects in Scene
	private List<GameObject> nodes;
	private List<GameObject> broadcasts;
	private GameObject MACbox;
	private BroadcastHandler bcastHandler;
	private GameObject captureTank0;
	private GameObject captureTank1;
	private GameObject crackWindow;
	private GameObject crackArrow;

	public ProgressHandlerL1 (	List<GameObject> a_nodes,
								List<GameObject> a_broadcasts,
								GameObject a_MACbox,
								GameObject a_captureTank_init,
								GameObject a_captureTank_post,
								GameObject a_crackWindow,
								GameObject a_crackArrow,
								Scrollbar a_assist_bar,
								Text a_assistance_text,
								Button a_advanceChat,
								Button a_retractChat)
	{
		nodes = a_nodes;
		broadcasts = a_broadcasts;
		MACbox = a_MACbox;
		captureTank0 = a_captureTank_init;
		captureTank1 = a_captureTank_post;
		crackWindow = a_crackWindow;
		crackArrow = a_crackArrow;
		assist_scrollbar = a_assist_bar;
		player_assistance_text = a_assistance_text;
		advanceChat = a_advanceChat;
		retractChat = a_retractChat;

		InitObjects ();
		InitChatData ();
		InitManualData ();
		bcastHandler = new BroadcastHandler(nodes, broadcasts);
	}



	// Draw objects relevant to current task
	public void incremenetScenePhase()
	{
		switch(currScenePhase)
		{
		case 0:
			//"Monitoring mode started. Showing broadcasts and AP MAC"

			// show router MAC addr 
			MACbox.SetActive (true);
			// show messages in transit
			bcastHandler.toggleBroadcastVis ();

			// progress to next phase
			currScenePhase++;
			incrementChatPage ();
			break;
		case 1:
			//"Handshake captured on wlan0. Data stored in captureFile.pak"

			// render captured broadcast(s) in tank
			captureTank0.SetActive (true);

			// progress to next phase
			currScenePhase++;
			incrementChatPage ();
			break;
		case 2:
			//"Cracking WPA key from captureFile.pak, weakPasswordList"

			//unrender nodes & broadcasts
			foreach (GameObject node in nodes)
				node.SetActive (false);
			bcastHandler.toggleBroadcastVis ();
			MACbox.SetActive (false);

			// move/rerender tank
			captureTank0.SetActive (false);
			captureTank1.SetActive (true);

			// render crackwindow
			crackWindow.SetActive (true);
			crackArrow.SetActive (true);

			// start timer to update Chat & scene phase after password is found
			StaticCoroutine.StartCoroutine(crackTimer());
			break;

			// transition to Exit scene (where pasword is entered) handled in Level1.cs
		}
	}


	public int getPaneCount()
	{
		return chatPanes.Count;
	}

	public int getCurrentPhase()
	{
		return currScenePhase;
	}

	public void incrementChatPage()
	{
		currAssistPane = Mathf.Min(currAssistPane + 1, getPaneCount() - 1);

		retractChat.gameObject.SetActive (true);
		if (getCurrentPane() >= getCurrentPhase())
			advanceChat.gameObject.SetActive (false);

		switchToChat ();
	}

	public void decrementChatPage()
	{
		currAssistPane = Mathf.Max(0, currAssistPane - 1);

		advanceChat.gameObject.SetActive (true);
		if (getCurrentPane() == 0)
			retractChat.gameObject.SetActive (false);

		switchToChat ();
	}

	public void switchToChat()
	{
		player_assistance_text.text = getChatPane (currAssistPane);
		assist_scrollbar.value = 1.0f;
	}

	public void switchToManual()
	{
		player_assistance_text.text = getManualPane (currAssistPane);
		//assist_scrollbar.value = 0;
	}


	private string getChatPane(int i)
	{
		return chatPanes [i];
	}

	private string getManualPane(int i)
	{
		return manPanes [i];
	}

	private int getCurrentPane()
	{
		return currAssistPane;
	}

	private void InitObjects()
	{
		foreach (GameObject node in nodes)
			node.SetActive (true);
		foreach (GameObject bcast in broadcasts)
			bcast.SetActive (false);
		MACbox.SetActive (false);
		captureTank0.SetActive (false);
		captureTank1.SetActive (false);
		crackWindow.SetActive (false);
		crackArrow.SetActive (false);
	}

	private void InitManualData()
	{
		 manPanes.Add( 
			"airmon-ng: a script to switch your wireless interface between modes"+
			"\n\n<start\\stop>\n    The first argument tells the script whether to start or stop monitoring mode"+
			"\n<interface>\n    The second argument tells the script what wireless interface you want to use."+
			"\n    You just have access to wlan0.");

		manPanes.Add(
			"airodump-ng: a tool for capturing segments of wireless broadcasts in the aircrack-ng suite"+
			"\n\n--bssid <MAC address>\n    This option tells the program to only consider conections running through the given MAC address"+
			"\n-w <file prefix>\n    This option tells the program to output captured data to files starting with the given prefix"+
			"\nFor now, go ahead and name this \"captureFile\"."+
			"\n<interface>\n    This argument tells the program which interface (in monitoring mode) it should use to capture data");

		manPanes.Add(
			"aircrack-ng: key cracking program for the connection protocol in use" +
			"\n\n-w <word list>\n    This is the dictionary of bad passwords the program will use in cracking the network key" +
			"\nYour friends have provided you with the file weakPasswordList." +
			"\n-b <MAC address>\n    This argument specifies that the target network is running on the given MAC address" +
			"\n<captured file(s)>\n    This argument, a file or set of files, tells the program what to analyse and crack" +
			"\nThis corresponds to the capture File you named earlier.");
	}

	private void InitChatData()
	{
		chatPanes.Add ("Ada: Okay, here we go. There are a bunch of computers trying to " +
		"connect to the router here. First, you need to be able to see the messages they’re " +
		"all sending. Airmon is a WiFi tool we’ve already installed for you. Run it, " +
		"and the messages will become visible!");

		chatPanes.Add ("Charles: Good job! Now that you can see the messages, you need to " +
		"catch some and copy them to a file. It doesn’t matter which computer. Anything " +
		"coming into the router can help us! That number on the router is its name. It’s " +
		"called a MAC Address. If you need to tell the router to do anything, use its name in the command!");

		chatPanes.Add ("Ada: Perfect! We've caught the message and put them in a file! These contain "+
		"all the informatin we need to find the password.  Now all we need to do is run the script to "+
		"compare the messages to the list of passwords, and we'll be through this door in no time!");

		chatPanes.Add ("Charles: That must be it! Go type it in!");

		//chatPanes.Add ("Charles: Now that you’re on the network, the door unlocked to let you through! " +
		//"Keep going, and be careful! Who knows what they might do to you if they find out you hacked into their network.");
	}

	IEnumerator crackTimer()
	{
//		Debug.Log("starting crackTimer");
		yield return new WaitForSeconds (5);
//		Debug.Log ("crackTimer complete, advancing");
		currScenePhase++;
		incrementChatPage ();
	}
}
