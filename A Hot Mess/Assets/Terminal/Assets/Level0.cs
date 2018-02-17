using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Level0 : MonoBehaviour
{
    public InputField inputfield;
    public Text output_text;
    public string keyword = " ";
    public Text chat_man;
    public Button chatbutton;
    public Button manualbutton;

    // Use this for initialization
    void Start()
    {
        chat();
        chatbutton.onClick.AddListener(() => chat());
        manualbutton.onClick.AddListener(() => manual());
        inputfield.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        string input = inputfield.text;

        List<string> options = new List<string>()
        {
            "10.240.13.76", "27.19.100.145", "100.102.63.74", "190.1.2.10",
            "201.2.5.100"
        };

        string[] split = input.Split(' ');
        

        if (Input.GetKeyDown(KeyCode.Return))
        {

            if (split.Length == 1 && split[0] == "iptables")
            {
                output_text.text += ">>  " + "That's the right command, but try using some of the options!" + "\n";
            }
            if (split.Length == 7 && split[0] == "iptables" && split[1] == "-A" && split[2] == "INPUT" && split[3] == "-s" && split[5] == "-j" &&
                split[6] == "DROP")
            {
                output_text.text += ">>  " + "Right command, wrong IP address!" + "\n";
                if(options.Contains(split[4]))
                {
                    output_text.text += "valid IP";
                }
            }
            
            inputfield.text = "";
            inputfield.ActivateInputField();
        }
    }


    void chat()
    {
        string[] chat_array = {"Charles: The box at the bottom is called a terminal. You can type commands into it and " +
            "press enter. Hints will appear in the manual in this box. Just click the manual tab an check it " +
            "out if you need any help! Click the chat button to talk to us again.", "Alan: Okay, see these IP addresses here? " +
            "Each one is a computer in this basement. " +
            "192.168.1.100 is mine, 192.168.1.101 is Ada’s computer, and 192.168.1.102 is Charles’ computer. " +
            "Yours is 192.168.1.103."};

        chat_man.text = chat_array[0];
    }

    void manual()
    {
        chat_man.text = "-iptables: This command lets you edit the blacklist." +
            "\n-Try using '-A INPUT' to add something new to the blacklist." +
            "\n-Typing '-s' and then an IP address tells the computer what source you are talking about." +
            "\n-Using '-j DROP' tells the computer to drop anything that comes from a certain source." +
            "\n-Put all of the commands on one line to add IP address to the blacklist!";
    }
}
