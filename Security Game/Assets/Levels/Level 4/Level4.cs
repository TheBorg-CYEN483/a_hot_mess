using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Level4 : MonoBehaviour
{
    public InputField inputfield;
    public Text output_text;
    public Text ciphertext1;
    public string ciphertext;
    public Text plaintext;
    public string keyword = " ";
    public Text chat_man;
    public Button chatbutton;
    public Button manualbutton;
    public int temp = 0;
    public string[] plaintemp = new string[235];



    /*  public Dictionary<string, int> letters = new Dictionary<string, int>{
          {"a", 97}, {"b", 98},{"c", 99},{"d", 100},{"e", 101},{"f", 102},{"g", 103},
          {" h", 104},{"i", 105},{"j", 106},{"k", 107},{"l", 108},{"m", 109},{"n", 110},
          {"o", 111},{"p", 112},{"q", 113},{"r", 114},{"s", 115},{"t", 116},{"u", 117},
          {"v", 118},{"w", 119},{"x", 120},{"y", 121},{"z", 122}};
          */

    // Use this for initialization
    void Start()
    {

        chatbutton.onClick.AddListener(() => chat());
        manualbutton.onClick.AddListener(() => manual());

        for (int i = 0; i < plaintemp.Length; i++)
        {
            plaintemp[i] = " ";
        }
    }

    // Update is called once per frame
    void Update()
    {
        string input = inputfield.text;
        int length = ciphertext.Length;
        ciphertext = @"yz zyp htww pgpc mp lmwp ez cplo estd xpddlrp

mpnlfdp te td pyncjaepo. t nly dlj hslepgpc t

hlye. lwly estyvd sp td dz dxlce. sp zywj slyrd

zfe htes std qctpyod. t lx esp mpde 

ncjaezrclaspc. yz zyp htww pgpc opntaspc estd.";
         string plain = @"no one will ever be able to read this message

because it is encrypted  i can say whatever i

want  alan thinks he is so smart  he only hangs

out with his friends  i am the best 

cryptographer  no one will ever decipher this ";

        
       

        string[] input_split = input.Split(' ');
        ciphertext1.text = ciphertext;
        string plaintempjoin = "";

        if (Input.GetKeyDown(KeyCode.Return))
        {
            for (int i = 0; i < length; i++)
             {
                 if (ciphertext[i].ToString() == input_split[0])
                 {
                     plaintemp[i] = input_split[2];
                     plaintempjoin += plaintemp[i];

                 }
                 else if(ciphertext[i] == '\n')
                 {
                     plaintemp[i] = "\n";
                     plaintempjoin += plaintemp[i];
                 }
                 else
                 {

                     plaintempjoin += plaintemp[i];
                 }
                 
            }

            plaintext.text = plaintempjoin;
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


//string1.compareto(string2)

