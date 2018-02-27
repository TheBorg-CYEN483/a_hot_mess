using System;
using System.Linq;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class ChatHandlerL1
	{
		private List<string> chatPanes = new List<string>();

		public ChatHandlerL1 ()
		{
			InitChatData ();
		}

		private void InitChatData()
		{
			/*
			chatPanes.Add ("-iptables: This command lets you edit the blacklist." +
			"\n-Try using '-A INPUT' to add something new to the blacklist." +
			"\n-Typing '-s' and then an IP address tells the computer what source you are talking about." +
			"\n-Using '-j DROP' tells the computer to drop anything that comes from a certain source." +
			"\n-Put all of the commands on one line to add IP address to the blacklist!");
*/
			chatPanes.Add ("Ada: Okay, here we go. There are a bunch of computers trying to " +
			"connect to the router here. First, you need to be able to see the messages they’re " +
			"all sending. Airmon is a WiFi tool we’ve already installed for you. Run it, " +
			"and the messages will become visible!");

			chatPanes.Add ("Charles: Good job! Now that you can see the messages, you need to " +
			"catch some and copy them to a file. It doesn’t matter which computer. Anything " +
			"coming into the router can help us! That number on the router is its name. It’s " +
			"called a MAC Address. If you need to tell the router to do anything, use its name in the command!");

			chatPanes.Add ("Ada: Perfect! We've caught the message and put them in a file! These contain "+
			"all the informatin we need to find the password.  Now all we need to do is run the script to "+
			"compare the messages to the list of passwords, and we'll be through this door in no time!");

			chatPanes.Add ("Charles: That must be it! Go type it in!");

			chatPanes.Add ("Charles: Now that you’re on the network, the door unlocked to let you through! " +
			"Keep going, and be careful! Who knows what they might do to you if they find out you hacked into their network.");

		}

		public string getChatPane(int i)
		{
			return chatPanes [i];
		}

		public int getPaneCount()
		{
			return chatPanes.Count;
		}
	}
}

