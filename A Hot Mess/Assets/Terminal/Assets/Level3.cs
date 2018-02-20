using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Level3  : MonoBehaviour
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
        chatbutton.onClick.AddListener(() => chat());
        manualbutton.onClick.AddListener(() => manual());
    }

    // Update is called once per frame
    void Update()
    {
        string input = inputfield.text;
        string shortened;

        
        List<string> MalChecker = new List<string>()
        {
            "--scan-all"
        };
        List<string> qoperation = new List<string>()
        {
            "drbackup -t Q_FULL"
        };

        string[] split = input.Split(' '); //Take the input and split at spaces
        foreach (var word in split)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                output_text.text += ">>  " + input;
                if (split[0] == "MalChecker")
                {
                    shortened = input.Replace("MalChecker ", "");
                    if (MalChecker[0].Contains(shortened))
                    {
                        output_text.text += "\nWorked \n.";
                        chat();
                    }
                }
            }
        }
    }

    void chat()
    {
       chat_man.text = "Ada: Good, now you can see the malware \n" +
                       "Charles: Luckily, MalChecker trapped them in the failure folder.\n" + "They can’t move anymore." + "\n Now you just need to remove the folder \n."
;
    }

    void manual()
    {
        chat_man.text = "scan" +
            "\n- This will begin looking through the" +
            "\n  folders. To setup the scan with MalChecker you would type" +
            "\n  Malchecker --scan" +
            "\n  all:" + 
            "\n- This will select everything in the" +
            "\n  space that you specify." + 
            "\n- For instance, scan-all will select" +
            "\n  everything";
    }
}
