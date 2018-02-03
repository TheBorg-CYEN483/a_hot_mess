using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Terminal : MonoBehaviour {
    public InputField inputfield;
    public Text text;
    public string keyword = " ";

    // Use this for initialization
    void Start() {
    }

   
    // Update is called once per frame
    void Update()
    {
        string name = inputfield.text;
       
        //if (Input.GetKeyDown(KeyCode.Return))
        //{

        //}
        List<string> wireshark = new List<string>()
        {
            "-v", "-k"
        };
        string[] split = name.Split(' ');
        
        foreach(var word in split)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (split[0] == "wireshark")
                {
                    name = name.Replace("wireshark ", "");
                    if (wireshark[0].Contains(name))
                    {
                        text.text = "This worked";
                    }                
                }
            }
        }
    }
}
