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
	private string prevCmd;

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

		// transition to Level Exit after successful cracking
		if (progressHandler.getCurrentPhase () == 3 &&
		    (Input.GetMouseButtonDown (0) ||
		    Input.GetKeyDown (KeyCode.Return) ||
		    Input.GetKeyDown (KeyCode.Space))) 
		{
			SceneManager.LoadScene ("Level 1_Exit");
			return;
		}
		
		// user input trigger
		string input = inputfield.text;
		if (Input.GetKeyDown(KeyCode.Return))
		{
			prevCmd = input;
			parseInput (input);
			inputfield.text = "";
		}
		inputfield.ActivateInputField ();
		if (Input.GetKeyDown (KeyCode.DownArrow))
			inputfield.text = "";
		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			inputfield.text = prevCmd;
			inputfield.caretPosition = inputfield.text.Length;
		}
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

		// ordered solution check with basic feedback
		int i = progressHandler.getCurrentPhase();
		List<string> solutionTokens = solutions [i].Split ().ToList();
		int j = 0;

		if (inputTokens.Count > solutionTokens.Count)
		{
			terminalLog ("Command not recognised.");
			return;
		}
		// fix this for flexibile output file in final version
		while (j < inputTokens.Count && solutionTokens[j] == inputTokens[j])
			j++;
		if (j == solutionTokens.Count())
		{
			incremenetScenePhase ();
			return;
		}

		switch (i) {
		case 0:
			switch (j) {
			case 0:
				terminalLog ("Command not recognised. Check the manual for more information.");
				return;
			case 1:
				terminalLog ("Command incomplete; airmon-ng needs directions");
				return;
			case 2:
				terminalLog ("Malformed command. Please specify a hardware interface.");
				return;
			}
			break;
		case 1:
			switch (j) {
			case 0:
				terminalLog ("Command not recognised. Check the manual for info about capturing data.");
				return;
			case 1:
				terminalLog ("Command incomplete; please specify options.");
				return;
			case 2:
				terminalLog ("Command incomplete; --bssid requires a MAC address.");
				return;
			case 3:
				terminalLog ("Command incomplete; airodump-ng needs an output destination.");
				return;
			case 4:
				if (inputTokens.Count >= 5 && inputTokens[4] != "")
					terminalLog ("Sorry, development version; rename file to 'captureFile'");
				else
					terminalLog ("Command incomplete; -w requires destination 'captureFile'");
				return;
			case 5:
				terminalLog ("Command incomplete; airodump requires network hardware to be specified.");
				return;
			}
			break;
		case 2:
			switch (j) {
			case 0:
				terminalLog ("Command not recognised. Check the manual for info about cracking keys.");
				return;
			case 1:
				terminalLog ("Command incomplete; please specify options.");
				return;
			case 2:
				terminalLog ("Command not recognised. -w needs a valid file.");
				return;
			case 3:
				terminalLog ("Command not incomplete; please specify a target.");
				return;
			case 4:
				terminalLog ("Command not recognised. -b needs a valid target.");
				return;
			case 5:
				terminalLog ("Command malformed; aircrack-ng needs a capture file specified.");
				return;
			}
			break;
		}
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

		Debug.Log (str.Length);
		int apprxLines = (int) Mathf.Ceil((str.Length + 1)/75f);
		linecounter += apprxLines;
		if (linecounter > 6)
		{
			scroller.value = 1f - (0.0007f * (float) (linecounter-5));
		}
		Debug.Log (apprxLines + ", " + linecounter + " -> " + scroller.value);
	}
}
