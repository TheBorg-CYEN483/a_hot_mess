using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2 : MonoBehaviour
{
    public InputField inputfield;
    public Text output_text;
    public string keyword = " ";
    public Text chat_man;
    public Button chatbutton;
    public Button manualbutton;
    public string openingDialog = "Charles: More locked doors. I guess we should have seen that coming. But it looks like this door is constantly receiving a signal to stay locked." +
        "\nAda: The signal is going through a router, or access point, in the room. Problem is, there are several other signals going through the access point going to different places. I can't narrow down what IP the signal could be coming from." +
        "\nCharles: I don't usually suggest this, but you will have to execute a Man-in-the-Middle attack to find, intercept, and modify the signal that is telling the door to stay locked." +
        "\nAda: We should look for another option first. [Name] could get in a lot of trouble for doing that." +
        "\nCharles: We don’t have that kind of time. Alan needs us. To do this, you need to find the IP address sending the signal that is keeping the door locked. Then, convince it to send the signal to your computer instead. When you receive the signal, modify it to unlock the door and send it to the access point. The tricky part is lying to both the access point and the door about where signals are coming from." +
        "\nAda: Your understanding of this frightens me, but if you think [Name] can do it then I won’t argue. This time." +
        "\nCharles: This is called a packet. Packets send information from one computer to the next. Think of packets like letters in the mail. They’re envelopes that carry information. When you use a tool like WireShark to view packets, you can see information about where the packet is going and who is sending it. It’s just like if you were looking at the destination address and the return address on an envelope. Viewing packets over a network like this is called packet sniffing.";

    // Use this for initialization
    void Start()
    {
        chatbutton.onClick.AddListener(() => chat());
        manualbutton.onClick.AddListener(() => manual());
    }

    // Update is called once per frame
    void Update()
    {
        string input = inputfield.text;
        string shortened;

        List<string> wireshark = new List<string>() {
            "-v", "-k"
        };

        string[] split = input.Split(' ');

        foreach (var word in split)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                output_text.text += ">>  " + input;
                if (split[0] == "wireshark")
                {
                    shortened = input.Replace("wireshark ", "");
                    if (wireshark[0].Contains(shortened))
                    {
                        output_text.text += "\nThis worked.";
                    }
                }
            }
        }
    }

    void chat()
    {
        chat_man.text = "Ada: This is what we are looking at. Several signals are coming into the access point and being sent to various locations. Try using that WireShark tool Charles mentioned." +
            "\n\nAda: Good job. We can see where the packets are going and who is sending them. Now we need the packet going to the door to go to your computer instead. It looks like the signal going from the router to the door is 10.0.6.7 to 10.0.5.2. To do this next part, you will have to use IP Spoofing. This means that you make your IP address look like another IP address. By doing this, other computers will see the spoofed IP address rather than your IP address. It’s kind of like wearing a costume, no one knows it’s really you in there. Also, now that we know what we are looking for, it may be easier to ignore the other packets." +
            "\n\nCharles: Good, you got the packet. Now view the details of the packet and modify it to an unlock signal. Then you will need to spoof your IP to look like the original senders and send it to the door." +
            "\n\nCharles: Great. The door should unlock now.";
    }

    void manual()
    {
        chat_man.text = "Wireshark is a free and open source packet analyzer. It is used to see the information being sent over a network." +
            "\n\nCommon Commands:" +
            "\n-Using '-k' shows where the packet is coming from and where the packet is going." +
            "\n-Using '-w -' displays the information from the packet in the terminal." +
            "\n\nThe nmap command is used to scan IP addresses and can be used to send and recieve packets over a network." +
            "\n\nCommon Commands:" +
            "\n-Using '-s [Source] [Destination]' will send all packets from the Source IP address to the Destination IP address" +
            "\n\nThe set command is used as a way to set variables in the terminal and in this case can be used to change information in a packet" +
            "\n\nUsage: 'set [Variable] = [Value]'";
    }
}