using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]	//This will let see the exit inside of the inspector. This will hold one exit. View this link to learn more: https://www.youtube.com/watch?v=-LlAahTMtjw
public class Exit  //Remove monobehavior to make ensure it is only displayed as a serial class
{
	public string keystring; // This is the string that will be accepted to move to the next exit, image, etc.
	public string exitDescription; // This will be a description of the room (we could maybe use this for something)
	public Room valueRoom;	// This will go in a dictionary (I'll figure it out later)
	public Exit[] exits;	//
}
