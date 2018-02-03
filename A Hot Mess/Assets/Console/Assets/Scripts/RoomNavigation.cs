using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour {

	public Room currentRoom;

	Dictionary<string, Room> exitDictionary = new Dictionary<string, Room> (); // string is type, room is value 

	GameController controller;

	void Awake()
	{
		controller = GetComponent<GameController> (); 
	}

	public void UnpackExitsInRoom()	//unpack exits and display them as a list of exits
	{
		for (int i = 0; i < currentRoom.exits.Length; i++) 
		{
			exitDictionary.Add (currentRoom.exits [i].keystring, currentRoom.exits [i].valueRoom); //This adds the keystring to the dictionary of acceptable strings for the exit. Sets the key and the value. Video 6/8 https://www.youtube.com/watch?v=g9Pjv-BWpso&list=PLX2vGYjWbI0RfcpqpKlmLEy7NteIog8g4&index=6
			controller.interactionDescriptionInRoom.Add (currentRoom.exits [i].exitDescription); //This will send the description to the list of exit descriptions
		}
	}

	public void AttemptToChangeRooms(string directionNoun)
	{

		if (exitDictionary.ContainsKey (directionNoun)) {  //Call this if what the player enters matches the keystring
			currentRoom = exitDictionary [directionNoun]; //This will change the room 
			controller.LogStringWithReturn ("You head off to the " + directionNoun); //We will likely need to recopy this for the dialogue that contains the character name.
			controller.DisplayRoomText ();
		} 
		else 
		{
			controller.LogStringWithReturn ("There is no path to the " + directionNoun); //This will be what will be seen when the 
		}
	}
		public void ClearExits() //Empties out the dictionary when moving to the next room. This will be good for removing the commands from the previous levels that the player may not need.
		{
			exitDictionary.Clear (); //This will be called from the GameController
		}
		
	
}
