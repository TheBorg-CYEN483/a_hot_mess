using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp; 	// for the inclusion of helper classes


public class Level1 : MonoBehaviour {

	// Progress Tracking
	private static GameObject displayContainer;
	private static int scenePhaseCount = 5;
	private int currSceneProgress = 0;
	private List<GameObject>[] sceneObjects;

	// Chat Data Repo
	private ChatHandlerL1 chatHandler;
	private int currChatPane = 0;


	// Terminal, Manual, Dialogue UI elements
	public InputField inputfield;
	public Text output_text;
	public Text player_assistance_text;
	public Button chatbutton;
	public Button manualbutton;
	public Button advanceChat;
	public Button retractChat;

	// Use this for initialization
	void Start()
	{
		// Utility Reference
		displayContainer = this.gameObject; 	
		chatHandler = new ChatHandlerL1 ();
		switchToChat ();

		// Initialise GameObjects for Level
		sceneObjects = new List<GameObject>[scenePhaseCount];
		for (var i = 0; i < scenePhaseCount; i++) { 	sceneObjects[i] = new List<GameObject> (); 		}
		initSceneObjectsList (sceneObjects);


		// UI Buttons
		chatbutton.onClick.AddListener(() => switchToChat());
		manualbutton.onClick.AddListener(() => switchToManual());
		advanceChat.onClick.AddListener(() => incrementChatPage ());
		retractChat.onClick.AddListener(() => decrementChatPage ());
	}

	// Update is called once per frame
	void Update()
	{
		string input = inputfield.text;

		if (Input.GetKeyDown(KeyCode.Return))
		{
			parseInput (input);
			inputfield.text = "";
			inputfield.ActivateInputField ();
		}
	}



	static void initSceneObjectsList(List<GameObject>[] target)
	{
		List<GameObject> objsToDraw = new List<GameObject>(); 	


		GameObject go = new GameObject ("Reference Sprite");
		Image im = go.AddComponent<Image> ();
		im.sprite = Resources.Load<Sprite>("Client_White-Full");
		im.color = Color.green;


		objsToDraw.Add (go);

		// make each object in the reference list a child of UI master container
		foreach (GameObject obj in objsToDraw)
		{
			target[0].Add( Instantiate (obj, displayContainer.transform));
		}
	}

	void drawScene(int i)
	{
		foreach (GameObject ob in sceneObjects[i]) 
		{
			terminalLog (ob.ToString () + ob.transform.position);
		}
	}

	void incremenetScenePhase()
	{
		currSceneProgress++;
		incrementChatPage ();
		drawScene (currSceneProgress);
	}

	void incrementChatPage()
	{
		currChatPane = Mathf.Min(currChatPane + 1, chatHandler.getPaneCount() - 1);
		switchToChat ();
	}

	void decrementChatPage()
	{
		currChatPane = Mathf.Max(0, currChatPane - 1);
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
	}

	void parseInput(string input)
	{
		// temporary trigger
		if (input == "") 
		{
			drawScene (currSceneProgress);
			return;
		}

		// basic setup for solution structure
		List<string> solutions = new List<string>()
		{
			"airmon-ng start wlan0", 
			"airodump-ng --bssid 80:2a:a8:17:74:b5 -w captureFile wlan0",
			"aircrack-ng -w weakPasswordList -b 80:2a:a8:17:74:b5 captureFile",
			"password: thebestpassword"
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

		// Return output based on successful check
		//  Temporary Description in the terminal output
		switch(currSceneProgress)
		{
		case 0:
			terminalLog ("Monitoring mode started. Show broadcasts and AP MAC");
			break;
		case 1:
			terminalLog ("Handshake captured on wlan0. Data stored in captureFile.pak");
			break;
		case 2:
			terminalLog ("Cracking WPA key from captureFile.pak, weakPasswordList");
			break;
		case 3:
			terminalLog ("Password Accepted; Level Solved");
			break;
		}

		incremenetScenePhase ();
	}
}