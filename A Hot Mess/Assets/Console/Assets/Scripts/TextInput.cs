using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour //This without recognition of the input will only mirror the text back to the player
{
	public InputField inputField; //Eventsystem is waiting for an event button i.e. enter

	GameController controller;

	void Awake()
	{
		controller = GetComponent<GameController> ();
		inputField.onEndEdit.AddListener (AcceptStringInput); //Once a UI elements, it will call this string
	}
	void AcceptStringInput(string userInput)
	{
		userInput = userInput.ToLower (); //This will convert all user input to lowercase
		controller.LogStringWithReturn (userInput);

		char[] delimiterCharacters = { ' ' }; // Log the text first. Spaces will be the characters used to separate strings. 
		string[] separatedInputWords = userInput.Split(delimiterCharacters); //Separate the characters witht eh spaces

		for (int i = 0; i < controller.inputActions.Length; i++) 
		{
			InputAction inputAction = controller.inputActions [i];
			if (inputAction.keyWord == separatedInputWords [0]) 
			{
				inputAction.RespondToInput (controller, separatedInputWords); //Looks for a keyword match. Pass in the array and the second word
			}
		}

		InputComplete (); // move to Input Complete
	}

	void InputComplete()
	{
		controller.DisplayLoggedText ();
		inputField.ActivateInputField (); //Reactivates the input field
		inputField.text = null; //Empty out the input field so they can type more
	}
		

}
