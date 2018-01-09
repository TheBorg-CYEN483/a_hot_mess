/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;

public delegate void CommandHandler(string[] parameters);

public class ConsoleController {
	#region Event declarations 
	//Communicates with the ConsoleView
	public delegate void LogChangedHandler(string[] log);
	public event LogChangedHandler logChanged; 

	//Used to make console invisible throughout a cutscene
	public delegate void InvisibleConsole(bool visible); 
	public event InvisibleConsole Changed_To_Invisible;
	// Information about each command
	class CommandRegistration {
		public string command {get; private set;} //get the command
		public CommandHandler handler {get; private set;} //Handle the command
		public string help {get; private set;} // Get the commands when "help" is typed

		public CommandRegistration(string command, CommandHandler handler, string help) {
			this.command = command;
			this.handler = handler;
			this.help = help;		
		}
	}

	const int LogLines = 50; //Number of lines that should be kept in the log.

	Queue<string> scrolling = new Queue<string>(LogLines); //First in First out. Output scrollback for  
	List<string> commandHistory = new List<string>(); //This will be a list of player's previous commands.
	Dictionary<string, CommandRegistration> commands = new Dictionary<string, CommandRegistration>(); 

	public string[] log { get; private set; } // Copy of the scrolling array. Used for the ConsoleView

	const string repeatCmdName = "!!"; //This will repeat the last command by the player, constant since it will be skipped if players are in the command history.


	public ConsoleController() 
	{	
		//Commands added with registerCommand(). Goes in the order as: name, implementation method, and help text.
		registerCommand("babble", babble, "Example command that demonstrates how to parse arguments. babble [word] [# of times to repeat]");
		registerCommand("help", help, "Print the help.");// calls on he regisiterCommand function to add in the "help" command 
		registerCommand("echo", echo, "Send back the arguments as an array (for testing)");	
		registerCommand ("restart", restart, "Restarts the game");
		registerCommand("hide", hide, "Hide the console.");					// foreign
		registerCommand(repeatCmdName, repeatCommand, "Repeat last command.");	// foreign
		registerCommand ("resetprefs", resetPrefs, "Reset and Saves Player preferences");
	}

	// This portion adds commands for use. I.e. the commands the player will use.
	void registerCommand(string command, CommandHandler handler, string help) {
		commands.Add (command, new CommandRegistration (command, handler, help));
	}

	public void appendLogLine(string line) {
		Debug.Log (line);

		if (scrolling.Count >= ConsoleController.LogLines) {
			scrolling.Dequeue (); // if scrolling reaches or exeeds 50, remove and return
		}
		scrolling.Enqueue (line); //Put in the line 

		log = scrolling.ToArray (); // copies the elements into a new log array

		if (logChanged != null) {
			logChanged (log);	//If the log has been changed by a valid reference, allow the log to change.
		}
	}

	public void runCommandString(string commandString) {
		appendLogLine ("$" + commandString);

		string[] commandSplit = parseArguements(commandString); // Parses the arguments for the command string
		string[] args = new string[0];
		if (commandSplit.Length < 1) {
			appendLogLine (string.Format ("Unable to process command '0' D:, type 'help' for assistance", commandString));	//this handles empty strings
			return;
		} else if (commandSplit.Length >= 2) {
			int num_arguments = commandSplit.Length - 1;
			args = new string[num_arguments]; // create args to be a new string to be of length num_arguments
			Array.Copy (commandSplit, 1, args, 0, num_arguments);// copies numbers from the array and pastes them into another array
		}
		runCommand(commandSplit[0].ToLower (), args);
		commandHistory.Add (commandString);
	}

	public void runCommand(string command, string[] args) {
		CommandRegistration reg = null;
		if (!commands.TryGetValue(command, out reg)) 
		{
			appendLogLine (string.Format ("Unknown command '{0}', bad command. Type 'help' for assistance", command)); // This line will handle wrong entries
		}
		else
		{
			if (reg.handler == null) {
				appendLogLine (string.Format ("Bad command '{0}', this is not a valid entry, plus empty stuff is not good either.", command));
			}
			else 
			{
				reg.handler(args); //If it is good, take the inputted string
			}
		}
}

	static string[] parseArguements(string commandString) //parse the commandstring with the input
	{
		LinkedList<char> parmChars = new LinkedList<char> (commandString.ToCharArray ()); //Linked list for character parameters
		bool Quotations = false; // set quotation to false for now
		var node = parmChars.First; //gets the first node in the linked list
		while (node != null)
		{
			var next = node.Next; //moves to the next node in the linked list
			if (node.Value == '"') // empty node values
			{
				Quotations = !Quotations;// set quotations to true
				parmChars.Remove (node); // remove the node
			}
			if (!Quotations && node.Value == ' ') 
			{
				node.Value = '\\';//The node value is now a new line. This just inserts a new line.
			}
			node = next; //move to the next node
		}
		char[] parmCharsArr = new char[parmChars.Count]; //New array that has parameters count.
		parmChars.CopyTo(parmCharsArr, 0);
		return (new string(parmCharsArr)).Split(new char[] {'\\'} , StringSplitOptions.RemoveEmptyEntries);
	}

	#region Command Hanlers
	//Implemented commands will be here
	// Will repeat the word a specified number of times.

	void babble(string[] args)
	{
		if (args.Length < 2) 
		{
			appendLogLine ("Expected 2 words for input.");
			return;
		}
		string text = args [0];
		if (string.IsNullOrEmpty (text)) {
			appendLogLine ("Expected text not to be empty");
		} 
		else 
		{
			int repeat = 0;
			if (!Int32.TryParse (args [1], out repeat)) 
			{
				appendLogLine ("Expected an integer for arg2.");
		
			} 
			else 
			{
				for (int i = 0; i < repeat; ++i) 
				{
					appendLogLine (string.Format ("{0} {1}", text, i));
				}
			}

		}

	}

	void echo(string[] args) 
	{
		StringBuilder string_to_build = new StringBuilder (); //This will be for building new strings that will echo arguments back as an array
		foreach (string arg in args) 
		{ // repeat for each element in the loop
			string_to_build.AppendFormat ("{0},", arg);
		}

		string_to_build.Remove(string_to_build.Length - 1, 1); //remove the specified number of characters from this index.
		appendLogLine(string_to_build.ToString());

	}

	void help(string[] args) {
		foreach (CommandRegistration reg in commands.Values) {
			appendLogLine (string.Format ("{0}: {1}", reg.command, reg.help));
		}
	}

	void hide(string[] args)
	{
		if (Changed_To_Invisible != null) {
			Changed_To_Invisible(false);
		}
	}

	void repeatCommand(string[] args) {
		for (int cmdIdx = commandHistory.Count - 1; cmdIdx >= 0; --cmdIdx) {
			string cmd = commandHistory [cmdIdx];
			if (String.Equals (repeatCmdName, cmd)) {
				continue;
			}
			runCommandString (cmd);
			break;
		}
	}
	void restart(string[] args) {
		Application.LoadLevel (Application.loadedLevel);
	}

	void resetPrefs(string[] args) {
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.Save ();
	}
	#endregion
}
#endregion
*/

using UnityEngine;

using System;
using System.Collections.Generic;
using System.Text;

public delegate void CommandHandler(string[] args);

public class ConsoleController {

	#region Event declarations
	// Used to communicate with ConsoleView
	public delegate void LogChangedHandler(string[] log);
	public event LogChangedHandler logChanged;

	public delegate void VisibilityChangedHandler(bool visible);
	public event VisibilityChangedHandler visibilityChanged;
	#endregion

	/// <summary>
	/// Object to hold information about each command
	/// </summary>
	class CommandRegistration {
		public string command { get; private set; }
		public CommandHandler handler { get; private set; }
		public string help { get; private set; }

		public CommandRegistration(string command, CommandHandler handler, string help) {
			this.command = command;
			this.handler = handler;
			this.help = help;
		}
	}

	/// <summary>
	/// How many log lines should be retained?
	/// Note that strings submitted to appendLogLine with embedded newlines will be counted as a single line.
	/// </summary>
	const int scrollbackSize = 20;

	Queue<string> scrollback = new Queue<string>(scrollbackSize);
	List<string> commandHistory = new List<string>();
	Dictionary<string, CommandRegistration> commands = new Dictionary<string, CommandRegistration>();

	public string[] log { get; private set; } //Copy of scrollback as an array for easier use by ConsoleView

	const string repeatCmdName = "!!"; //Name of the repeat command, constant since it needs to skip these if they are in the command history

	public ConsoleController() {
		//When adding commands, you must add a call below to registerCommand() with its name, implementation method, and help text.
		registerCommand("babble", babble, "Example command that demonstrates how to parse arguments. babble [word] [# of times to repeat]");
		registerCommand("echo", echo, "echoes arguments back as array (for testing argument parser)");
		registerCommand("help", help, "Print this help.");
		registerCommand("hide", hide, "Hide the console.");
		registerCommand(repeatCmdName, repeatCommand, "Repeat last command.");
		registerCommand("reload", reload, "Reload game.");
		registerCommand("resetprefs", resetPrefs, "Reset & saves PlayerPrefs.");
	}

	void registerCommand(string command, CommandHandler handler, string help) {
		commands.Add(command, new CommandRegistration(command, handler, help));
	}

	public void appendLogLine(string line) {
		Debug.Log(line);

		if (scrollback.Count >= ConsoleController.scrollbackSize) {
			scrollback.Dequeue();
		}
		scrollback.Enqueue(line);

		log = scrollback.ToArray();
		if (logChanged != null) {
			logChanged(log);
		}
	}

	public void runCommandString(string commandString) {
		appendLogLine("$ " + commandString);

		string[] commandSplit = parseArguments(commandString);
		string[] args = new string[0];
		if (commandSplit.Length < 1) {
			appendLogLine(string.Format("Unable to process command '{0}'", commandString));
			return;

		}  else if (commandSplit.Length >= 2) {
			int numArgs = commandSplit.Length - 1;
			args = new string[numArgs];
			Array.Copy(commandSplit, 1, args, 0, numArgs);
		}
		runCommand(commandSplit[0].ToLower(), args);
		commandHistory.Add(commandString);
	}

	public void runCommand(string command, string[] args) {
		CommandRegistration reg = null;
		if (!commands.TryGetValue(command, out reg)) {
			appendLogLine(string.Format("Unknown command '{0}', type 'help' for list.", command));
		}  else {
			if (reg.handler == null) {
				appendLogLine(string.Format("Unable to process command '{0}', handler was null.", command));
			}  else {
				reg.handler(args);
			}
		}
	}

	static string[] parseArguments(string commandString)
	{
		LinkedList<char> parmChars = new LinkedList<char>(commandString.ToCharArray());
		bool inQuote = false;
		var node = parmChars.First;
		while (node != null)
		{
			var next = node.Next;
			if (node.Value == '"') {
				inQuote = !inQuote;
				parmChars.Remove(node);
			}
			if (!inQuote && node.Value == ' ') {
				node.Value = '\\';
			}
			node = next;
		}
		char[] parmCharsArr = new char[parmChars.Count];
		parmChars.CopyTo(parmCharsArr, 0);
		return (new string(parmCharsArr)).Split(new char[] {'\\'} , StringSplitOptions.RemoveEmptyEntries);
	}

	#region Command handlers
	//Implement new commands in this region of the file.

	/// <summary>
	/// A test command to demonstrate argument checking/parsing.
	/// Will repeat the given word a specified number of times.
	/// </summary>
	void babble(string[] args) {
		if (args.Length < 2) {
			appendLogLine("Expected 2 arguments.");
			return;
		}
		string text = args[0];
		if (string.IsNullOrEmpty(text)) {
			appendLogLine("Expected arg1 to be text.");
		}  else {
			int repeat = 0;
			if (!Int32.TryParse(args[1], out repeat)) {
				appendLogLine("Expected an integer for arg2.");
			}  else {
				for(int i = 0; i < repeat; ++i) {
					appendLogLine(string.Format("{0} {1}", text, i));
				}
			}
		}
	}

	void echo(string[] args) {
		StringBuilder sb = new StringBuilder();
		foreach (string arg in args)
		{
			sb.AppendFormat("{0},", arg);
		}
		sb.Remove(sb.Length - 1, 1);
		appendLogLine(sb.ToString());
	}

	void help(string[] args) {
		foreach(CommandRegistration reg in commands.Values) {
			appendLogLine(string.Format("{0}: {1}", reg.command, reg.help));
		}
	}

	void hide(string[] args) {
		if (visibilityChanged != null) {
			visibilityChanged(false);
		}
	}

	void repeatCommand(string[] args) {
		for (int cmdIdx = commandHistory.Count - 1; cmdIdx >= 0; --cmdIdx) {
			string cmd = commandHistory[cmdIdx];
			if (String.Equals(repeatCmdName, cmd)) {
				continue;
			}
			runCommandString(cmd);
			break;
		}
	}

	void reload(string[] args) {
		Application.LoadLevel(Application.loadedLevel);
	}

	void resetPrefs(string[] args) {
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
	}

	#endregion
}