using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Go")]
public class Go : InputAction 
{ //This will inherit from input action and actions can be their own classes for ease of use for updating the game and adding content. 

	public override void RespondToInput (GameController controller, string[] separatedInputWords)
	{
		controller.roomNavigation.AttemptToChangeRooms (separatedInputWords [1]); 	//In general, we want to pass in objects so we don't have to create variables for objects. Passing in the second word
	}

}
