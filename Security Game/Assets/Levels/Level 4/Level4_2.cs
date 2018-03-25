using System.Collections;
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
    public Text alan;
    public Image meme;
    public int count;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        List<string> options = new List<string>
        {
            ("Cat\\ Videos"),
            ("Error\\ 404\\ Not\\ Found"),
            ("Information\\ on\\ How\\ to\\ Pixilate\\ Someone"),
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
                        if (input_parse[3] == "alan")
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
                        output_text.text = "Alan is in the 'Viruses' folder";
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

                if (new_text == "Viruses")
                {
                    panel.color = new Color(255, 255, 255, 255);
                    alan.text = "Alan.jpg";
                    count = 1;
                }
                else if(new_text == "Memes")
                {
                    meme.color = new Color(255, 255, 255, 255);
                }
                else if(options[0].Contains(new_text) || options[1].Contains(new_text) || options[2].Contains(new_text) || options[3].Contains(new_text) || options[4].Contains(new_text))
                {
                    output_text.text = "Alan isn't here";
                }
                else if (new_text == "..")
                {
                    panel.color = new Color(255, 255, 255, 0);
                    meme.color = new Color(255, 255, 255, 0);
                    alan.color = new Color(255, 255, 255, 0);

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
            
        }

	}
    void winner()
    {
        SceneManager.LoadScene(("Level4badge"));
        
    }
}
