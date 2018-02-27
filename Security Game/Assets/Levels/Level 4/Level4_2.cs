using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Level4_2 : MonoBehaviour {

    public InputField inputfield;
    public Text output_text;
    public Text chat_man;
    public Button chatbutton;
    public Button manualbutton;
    public Image panel;
    public Text alan;
    public Image meme;
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
            if (input_parse[0] == "cd")
            {
                new_text = input_text.Replace("cd ", "");

                if (new_text == "Viruses")
                {
                    panel.color = new Color(255, 255, 255, 255);
                    alan.text = "Alan.jpg";
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
                }
            }
        }

	}
}
