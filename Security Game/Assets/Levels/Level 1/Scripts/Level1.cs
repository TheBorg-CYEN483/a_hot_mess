using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Level1 : MonoBehaviour
{
	// Teaching Elements, Progression Data, Dynamic Elements
	private ProgressHandlerL1 progressHandler;

	// Terminal, Buttons, etc. Interactable UI elements
	public GameObject levelScreen;
	public InputField inputfield;
	public Text output_text;
	public Button chatbutton;
	public Button manualbutton;
	public Button advanceChat;
	public Button retractChat;
	private int linecounter = 0;
	public Scrollbar scroller;

	// Dynamic Objects in Scene
	public List<GameObject> nodes;
	public List<GameObject> broadcasts;
	public Scrollbar assist_scrollbar;
	public Text player_assistance_text;
	public GameObject MACbox;
	public GameObject captureTank_init;
	public GameObject captureTank_post;
	public GameObject crackWindow;
	public GameObject crackArrow;

	// Use this for initialization
	void Start()
	{
		// Utility Reference
		//   (read: horribly coupled spaghetti)
		progressHandler = new ProgressHandlerL1 (	nodes, broadcasts, MACbox, captureTank_init, 
			captureTank_post, crackWindow, crackArrow, 
			assist_scrollbar, player_assistance_text, advanceChat, retractChat);	

		// UI Buttons
		chatbutton.onClick.AddListener(() => progressHandler.switchToChat());
		manualbutton.onClick.AddListener(() => progressHandler.switchToManual());
		advanceChat.onClick.AddListener(() => progressHandler.incrementChatPage ());
		retractChat.onClick.AddListener(() => progressHandler.decrementChatPage ());

		progressHandler.switchToChat ();
	}

	// Update is called once per frame
	void Update()
	{
		// level splash screen, active removal
		if (levelScreen.activeSelf && Input.GetMouseButtonDown (0))
			levelScreen.SetActive (false);

		// user input trigger
		string input = inputfield.text;
		if (Input.GetKeyDown(KeyCode.Return))
		{
			parseInput (input);
			inputfield.text = "";
			inputfield.ActivateInputField ();
		}

		// transition to Level Exit after successful cracking
		if (progressHandler.getCurrentPhase () == 3 &&
		    (Input.GetMouseButtonDown (0) ||
		    Input.GetKeyDown (KeyCode.Return) ||
		    Input.GetKeyDown (KeyCode.Space)))
			SceneManager.LoadScene ("Level 1_Exit");
	}


	void parseInput(string input)
	{
		// basic setup for solution structure
		List<string> solutions = new List<string>()
		{
			"airmon-ng start wlan0", 
			"airodump-ng --bssid 80:2a:a8:17:74:b5 -w captureFile wlan0",
			"aircrack-ng -w weakPasswordList -b 80:2a:a8:17:74:b5 captureFile"
		};

		// echo and tokenise input
		terminalLog(">>  " + input);
		List<string> inputTokens = input.Split(' ').ToList();

		// crude ordered solution check; bad for detailed feedback
		for (var j = 0; j < inputTokens.Count; j++) 
		{
			if (solutions [progressHandler.getCurrentPhase()].Split () [j] != inputTokens [j]) 
			{
				terminalLog ("Input Error");
				return;
			}
		}

		// correct command entered, so:
		incremenetScenePhase ();
	}

	// Output description in the terminal output
	// Use progressHandler to track progress and update objects in scene
	void incremenetScenePhase()
	{
		switch(progressHandler.getCurrentPhase())
		{
		case 0:
			terminalLog ("Monitoring mode started. Showing broadcasts and AP MAC");
			break;
		case 1:
			terminalLog ("Handshake captured on wlan0. Data stored in captureFile.pak");
			break;
		case 2:
			terminalLog ("Cracking WPA key from captureFile.pak, weakPasswordList");
			break;

			// "case 3" requires no terminal input, just transitions to Exit Scene
		}
		progressHandler.incremenetScenePhase ();
	}

	// Output string to Terminal with autoscroll
	// note: long output lines cause issue with autoscroll
	void terminalLog(string str)
	{
		output_text.text += str + "\n";

		linecounter += 1;
		if (linecounter > 4)
		{
			scroller.value -= (float).065;
		}
	}
}
