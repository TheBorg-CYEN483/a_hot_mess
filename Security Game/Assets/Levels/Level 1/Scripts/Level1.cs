using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp; 	// for the inclusion of helper classes


public class Level1 : MonoBehaviour {

	// Progress Tracking
	//private static GameObject displayContainer;
	private static int scenePhaseCount = 5;
	private int currSceneProgress = 0;

	// Chat Data Repo
	private ChatHandlerL1 chatHandler;
	private int currChatPane = 0;


	// Terminal, Manual, Dialogue, etc. UI elements
	public GameObject levelScreen;
	public InputField inputfield;
	public Text output_text;
	public Text player_assistance_text;
	public Button chatbutton;
	public Button manualbutton;
	public Button advanceChat;
	public Button retractChat;
	private int linecounter = 0;
	public Scrollbar scroller;

	// Dynamic Objects in Scene
	public List<GameObject> nodes;
	public GameObject MACbox;
	private BroadcastHandler bcastHandler;
	public GameObject captureTank;
	public GameObject crackWindow;

	// Use this for initialization
	void Start()
	{
		// Utility Reference
		//displayContainer = this.gameObject; 	
		chatHandler = new ChatHandlerL1 ();
		switchToChat ();
		bcastHandler = new BroadcastHandler (nodes);

		// UI Buttons
		chatbutton.onClick.AddListener(() => switchToChat());
		manualbutton.onClick.AddListener(() => switchToManual());
		advanceChat.onClick.AddListener(() => incrementChatPage ());
		retractChat.onClick.AddListener(() => decrementChatPage ());
	}

	// Update is called once per frame
	void Update()
	{
		// level splash screen, active removal
		if (levelScreen.activeSelf && Input.GetMouseButtonDown (0))
			levelScreen.SetActive (false);
		
		string input = inputfield.text;

		if (Input.GetKeyDown(KeyCode.Return))
		{
			parseInput (input);
			inputfield.text = "";
			inputfield.ActivateInputField ();
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


		terminalLog(">>  " + input);
		List<string> inputTokens = input.Split(' ').ToList();

		// crude ordered solution check; bad for detailed feedback
		for (var j = 0; j < inputTokens.Count; j++) 
		{
			if (solutions [currSceneProgress].Split () [j] != inputTokens [j]) 
			{
				terminalLog ("Input Error");
				return;
			}
		}

		// correct command entered
		incremenetScenePhase ();
	}

	void incremenetScenePhase()
	{
		currSceneProgress++;
		incrementChatPage ();

		// Output description in the terminal output
		// Draw objects relevant to current task
		switch(currSceneProgress)
		{
		case 0:
			terminalLog ("Monitoring mode started. Showing broadcasts and AP MAC");
			MACbox.SetActive (true);
			captureTank.SetActive (true);
			bcastHandler.toggleBroadcastVis ();
			break;
		case 1:
			terminalLog ("Handshake captured on wlan0. Data stored in captureFile.pak");
			// render captured broadcast in tank
			break;
		case 2:
			terminalLog ("Cracking WPA key from captureFile.pak, weakPasswordList");
			// unrender nodes
			// move tank
			// render crackwindow
			break;

			// wait for completion
			// switch scene to Level 1 Exit

			// todo, remove this last case for final implementation
		case 3:
			terminalLog ("Password Accepted; Level Solved");
			break;
		}
	}

	void incrementChatPage()
	{
		retractChat.gameObject.SetActive (true);
		currChatPane = Mathf.Min(currChatPane + 1, chatHandler.getPaneCount() - 1);
		if (currChatPane >= currSceneProgress)
			advanceChat.gameObject.SetActive (false);
		switchToChat ();
	}

	void decrementChatPage()
	{
		advanceChat.gameObject.SetActive (true);
		currChatPane = Mathf.Max(0, currChatPane - 1);
		if (currChatPane == 0)
			retractChat.gameObject.SetActive (false);
		switchToChat ();
	}

	void switchToChat()
	{
		player_assistance_text.text = chatHandler.getChatPane (currChatPane);
	}

	void switchToManual()
	{
		player_assistance_text.text = 
			"airmon-ng: a script to switch your wireless interface between modes"+
			"\n\n<start\\stop>\n    The first argument tells the script whether to start or stop monitoring mode"+
			"\n<interface>\n    The second argument tells the script what wireless interface you want to use."+
			"\n    You just have access to wlan0.";


		player_assistance_text.text += 
			"\n\n\n\nairodump-ng: a tool for capturing segments of wireless broadcasts in the aircrack-ng suite"+
			"\n\n--bssid <MAC address>\n    This option tells the program to only consider conections running through the given MAC address"+
			"\n-w <file prefix>\n    This option tells the program to output captured data to files starting with the given prefix"+
			"\nYou can name this as you please."+
			"\n<interface>\n    This argument tells the program which interface (in monitoring mode) it should use to capture data";

		player_assistance_text.text += 
			"\n\n\n\naircrack-ng: key cracking program for the connection protocol in use" +
			"\n\n-w <word list>\n    This is the dictionary of bad passwords the program will use in cracking the network key" +
			"\nYour friends have provided you with the file weakPasswordList." +
			"\n-b <MAC address>\n    This argument specifies that the target network is running on the given MAC address" +
			"\n<captured file(s)>\n    This argument, a file or set of files, tells the program what to analyse and crack" +
			"\nThis corresponds to the file prefix you specified earlier.";
	}

	void terminalLog(string str)
	{
		output_text.text += str + "\n";

		linecounter += 1;
		if (linecounter > 4)
		{
			scroller.value -= (float).06;
		}
	}
}