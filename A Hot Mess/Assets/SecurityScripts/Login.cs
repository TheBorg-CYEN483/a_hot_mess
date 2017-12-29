// Login tutorial from https://www.youtube.com/watch?v=h9Fv9b39_tw used as starting point and guide.

using UnityEngine;
using System.Collections;

public class Login : MonoBehaviour {
	private string user = "Username";
	private string pass = "Password";

	private string usernameString = string.Empty;
	private string passwordString = string.Empty;

	private bool incorrect = false;
	private bool displayInspector = false;
	private bool access = false;
	private int count = 0;
	private float time = 0.0f;

	private Rect windowRect = new Rect(0, 0, Screen.width, Screen.height);
	private GUIStyle redTextStyle = new GUIStyle();
	private GUIStyle greenTextStyle = new GUIStyle();

	// Use this for initialization
	void Start () {
		redTextStyle.normal.textColor = Color.red;
		redTextStyle.fontSize = 20;

		greenTextStyle.normal.textColor = Color.green;
		greenTextStyle.fontSize = 50;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - time > 2) {
			incorrect = false;
		}

		if (Input.GetKeyDown(KeyCode.F12)) {
			displayInspector = !displayInspector;
		}

		if (Input.GetKeyDown("return")) {
			if ((usernameString == user) && (passwordString == pass)) {
				access = true;
			} else {
				incorrect = true;
				time = Time.time;
				count++;
			}
		}
	}

	void OnGUI () {
		GUI.Window(0, windowRect, WindowFunction, "Login");

		if (displayInspector) {
			GUI.ModalWindow(1, new Rect(2 * Screen.width / 3, 0, Screen.width / 3, Screen.height), InspectorModalFunction, "Inspector");
		}

		if (access) {
			GUI.ModalWindow(2, new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), AccessGrantedModalFunction, "Welcome to SSHhh...");
		}
	} 

	void WindowFunction (int windowId) {
		// User input login
		usernameString = GUI.TextField(new Rect(Screen.width / 3, 2 * Screen.height / 5, Screen.width / 3, Screen.height / 10), usernameString, 15);

		// User input password
		passwordString = GUI.PasswordField(new Rect(Screen.width / 3, 2 * Screen.height / 3, Screen.width / 3, Screen.height / 10), passwordString, "*"[0], 15);

		if (GUI.Button(new Rect(4 * Screen.width / 9, 4 * Screen.height / 5, Screen.width / 8, Screen.height / 8), "Login")) {
			if ((usernameString == user) && (passwordString == pass)) {
				access = true;
			} else {
				incorrect = true;
				time = Time.time;
				count++;
			}
		}

		GUI.Label(new Rect(Screen.width / 3, 34 * Screen.height / 100, Screen.width / 5, Screen.height / 8), "Username:");
		GUI.Label(new Rect(Screen.width / 3, 61 * Screen.height / 100, Screen.width / 5, Screen.height / 8), "Password:");

		if (incorrect) {
			GUI.Label(new Rect(4 * Screen.width / 11, 27 * Screen.height / 100, Screen.width / 3, Screen.height / 8), "Incorrect username or password", redTextStyle);
		}

		if (count == 2) {
			GUI.Label(new Rect(4 * Screen.width / 11, 20 * Screen.height / 100, Screen.width / 3, Screen.height / 8), "Hint: Try inspecting the page.", redTextStyle);
		} else if (count == 4) {
			GUI.Label(new Rect(4 * Screen.width / 11, 20 * Screen.height / 100, Screen.width / 3, Screen.height / 8), "Hint: You can inspect the page by pressing F12.", redTextStyle);
		}
	}

	void InspectorModalFunction (int windowId) {
		GUI.Label(new Rect(10, 20, Screen.width / 5, Screen.height / 8), "Username: " + user);
		GUI.Label(new Rect(10, 40, Screen.width / 5, Screen.height / 8), "Password: " + pass);
	}

	void AccessGrantedModalFunction (int windowId) {
		GUI.Label(new Rect(20, 50, Screen.width / 2, Screen.height / 2), "Access Granted", greenTextStyle);
	}
}
