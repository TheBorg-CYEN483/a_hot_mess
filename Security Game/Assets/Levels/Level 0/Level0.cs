using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Level0 : MonoBehaviour
{
    public static Level0 Instance;

    public InputField inputfield;
    public Text output_text;
    public Scrollbar scroller;
    public Text blacklist;
    public string keyword = " ";
    public Text chat_man;
    public Button chatbutton;
    public Button manualbutton;
    public Image blackoutScreen;
    public Text Level_0;
    List<string> options = new List<string>()
        {
            "10.240.13.76", "27.19.100.145", "100.102.63.74", "190.1.2.10",
            "201.2.5.100"
        };
    List<string> blocked = new List<string>() { };
    int linecounter = 0;


    // Use this for initialization
    void Start()
    {
        // levelScreen();
        Destroy(Level_0, 3);
        Destroy(blackoutScreen, 3);
        Instance = this;
        inputfield.ActivateInputField();
        chat();
        chatbutton.onClick.AddListener(() => chat());
        manualbutton.onClick.AddListener(() => manual());
        inputfield.ActivateInputField();
        
    }

    // Update is called once per frame
    void Update()
    {
        string input = inputfield.text;
        string[] split = input.Split(' ');

        if (options.Count == 0)
        {
            SceneManager.LoadScene(("Level0Badge"));
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {

            if (split.Length == 1 && split[0] == "iptables")
            {
                output_text.text += ">>  " + "Try using some of the options!" + "\n";
                linecounter += 1;
                if (linecounter > 4)
                {
                    scroller.value -= (float).06;
                }
            }
            if (split.Length == 7 && split[0] == "iptables" && split[1] == "-A" && split[2] == "INPUT" && split[3] == "-s" && split[5] == "-j" &&
                split[6] == "DROP")
            {
                if(options.Contains(split[4]))
                {
                    output_text.text += ">>  " + input + "\n";
                    blocked.Add(split[4]);
                    blacklist.text += "  " + split[4] + "\n";
                    options.Remove(split[4]);
                }
                else
                {
                    output_text.text += ">>  " + "Right command, wrong IP address!" + "\n";
                }

                linecounter += 1;
                if (linecounter > 4)
                {
                    scroller.value -= (float) .06;
                }
            }

            inputfield.text = "";
        }
        inputfield.ActivateInputField();
    }

    public static bool helper(string name)
    {
        return Level0.Instance.blocked.Contains(name);
    }
    void chat()
    {
        string[] chat_array = {"Charles: The box at the bottom is called a terminal. You can type commands into it and " +
            "press enter. \n\nHints will appear in the manual in this box. Just click the manual tab and check it " +
            "out if you need any help! Click the chat button to talk to us again." };

        chat_man.text = chat_array[0];
        inputfield.ActivateInputField();
    }

    void manual()
    {
        chat_man.text = "iptables: This command lets you edit the blacklist." +
            "\n\nTry using '-A INPUT' to add something new to the blacklist." +
            "\n\nTyping '-s' and then an IP address tells the computer what source you are talking about." +
            "\n\nUsing '-j DROP' tells the computer to drop anything that comes from a certain source." +
            "\n\nPut all of the commands on one line to add IP address to the blacklist!\n";
        inputfield.ActivateInputField();
    }
}
