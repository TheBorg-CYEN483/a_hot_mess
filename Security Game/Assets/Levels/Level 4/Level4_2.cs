﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Level4_2 : MonoBehaviour {

    public InputField inputfield;
    public Text output_text;
    public Text chat_man;
    public Button chatbutton;
    public Button manualbutton;
    public Image panel;
    public Image alanpic;
    public Text alan;
    public Image meme;
    public int count;
    int level = 0;

	// Use this for initialization
	void Start () {
        chatbutton.onClick.AddListener(() => chat());               //chat button
        manualbutton.onClick.AddListener(() => manual());           //manual button
        chat();
        inputfield.text = "";
        inputfield.ActivateInputField();
    }
	
	// Update is called once per frame
	void Update () {

        List<string> options = new List<string>
        {
            ("Cat\\ Videos"),
            ("Error\\ 404\\ Not\\ Found"),
            ("Information\\ on\\ How\\ to\\ Pixelate\\ Someone"),
            ("Pictures"),
            ("Totally\\ Not\\ Alan")
        };
        string input_text = inputfield.text;
        string new_text = "";
        string[] input_parse = input_text.Split(' ');
        if (Input.GetKeyDown(KeyCode.Return))
        {

            if(input_parse[0] == "sudo")
            {
                if (input_parse[1] == "find")
                {
                    if(input_parse[2] == "-iname")
                    {
                        if (input_parse[3] == "alan" || input_parse[3] == "Alan")
                        {
                            output_text.text = "Alan is in the Viruses folder.";
                        }
                    }
                    else if(input_parse[2] == "alan")
                    {
                        output_text.text = "There is no file named 'alan'. Try using '-iname' after 'find' so the terminal ignores case.";
                    }
                    else if (input_parse[2] == "Alan")
                    {
                        output_text.text = "Alan is in the Viruses folder.";
                    }
                }
            }
            else if(input_parse[0] == "find")
            {
                output_text.text = "You do not have sudo priveleges.";
            }

            if (input_parse[0] == "cd")
            {
                new_text = input_text.Replace("cd ", "");

                if (new_text == "Viruses" && level == 0)
                {
                    panel.color = new Color(255, 255, 255, 255);
                    alan.color = new Color(0, 0, 0, 255);
                    alanpic.color = new Color(255, 255, 255, 255);
                    alan.text = "Alan.jpg";
                    count = 1;
                    level = 1;
                }
                else if(new_text == "Memes" && level == 0)
                {
                    meme.color = new Color(255, 255, 255, 255);
                    level = 1;
                }
                else if(options[0].Equals(new_text) || options[1].Equals(new_text) || options[2].Equals(new_text) || options[3].Equals(new_text) || options[4].Equals(new_text))
                {
                    output_text.text = "Alan isn't here";
                }
                else if (new_text == "..")
                {
                    panel.color = new Color(255, 255, 255, 0);
                    meme.color = new Color(255, 255, 255, 0);
                    alan.color = new Color(255, 255, 255, 0);
                    alanpic.color = new Color(255, 255, 255, 0);
                    level = 0;
                    count = 0;
                }
            }

            if (input_text != "open Alan.jpg")
            {
                if (input_parse[0] == "open" && input_parse[1] == "Alan.jpg" && count == 1)
                {
                    if (input_parse[2] == "-p")
                    {
                        if (input_parse[3] == "HACKER")
                        {
                            winner();
                        }
                        else
                        {
                            output_text.text = "That is not the correct password";
                        }
                    }
                }
            }
            else
            {
                output_text.text = "This file is password protected.";
            }
            inputfield.text = "";
            inputfield.ActivateInputField();
        }

	}
    void winner()
    {
        SceneManager.LoadScene(("Level4badge"));
        
    }

    
    void chat()
    {
        chat_man.text = "Charles: Great! We got the password! We don't know what it goes to yet,"
    +  " but I think right now we just need to access their file system and FIND Alan. Ada, don't we"
    + "have a command for that? \n\nAda: Shh, Charles. They know how to use the manual. Let them figure it out.";
    }
    
    void manual()
    {
        chat_man.text = "sudo: gives administrative privileges \n \nfind: searches the file system for a file \n \n-iname:"
            + " a parameter for 'find' to allow it to ignore how the file name is typed \n\ncd: when followed by the folder name,"
            + "cd allows you to go to that folder\n \n '\\' + space: allows you to have spaces in a file name. \nex: cd Funny\\ Pictures" 
            + "\n\nopen: opens a file \n\n-p: allows you to type a password after the file name when using the 'open' command";
    }
}
