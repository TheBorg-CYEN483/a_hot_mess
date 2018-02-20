using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Terminal_Raw_020618 : MonoBehaviour {
    public InputField inputfield;
    public Text output_text;
    public string keyword = " ";
    public Text chat_man;
    public Button chatbutton;
    public Button manualbutton;

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
       
        //if (Input.GetKeyDown(KeyCode.Return))
        //{

        //}
        List<string> wireshark = new List<string>()
        {
            "-v", "-k"
        };

        string[] split = input.Split(' ');
        
        foreach(var word in split)
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
        chat_man.text = "other blah blah blah";
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
