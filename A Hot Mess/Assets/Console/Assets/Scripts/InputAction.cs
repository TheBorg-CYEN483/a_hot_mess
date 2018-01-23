using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputAction : ScriptableObject
{
	public string keyWord; // this will be the string that will be responded to

	public abstract void RespondToInput (GameController controller, string[] separatedInputWords);
}
