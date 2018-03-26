using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Level4 : MonoBehaviour
{
    public InputField inputfield;
    public Text output_text;
    public Text ciphertext1;
    public Text plaintext;
    public Text plaintext_alphabet;
    public Text ciphertext_alphabet;
    public Text chat_man;
    public Text Level_4;
    public Button chatbutton;
    public Button manualbutton;
    public Button shiftbutton;
    public Button right_button;
    public Image blackoutScreen;
    public int temp = 0;
    public string plain = "";
    public string compare = "";
    public char[] crypto = new char[235];
    public string[] plaintemp = new string[235];
    public System.Random rnd = new System.Random();


    // Use this for initialization
    void Start()
    {
        right_button.interactable = false;
        chat();
        plain = @"no one will ever be able to read this message

because it is encrypted. i can say whatever i

want. alan thinks he is so smart. he only hangs

out with his friends. i am the best 

cryptographer. no one will ever decipher this.";

        compare = @"no one will ever be able to read this message

because it is encrypted  i can say whatever i

want  alan thinks he is so smart  he only hangs

out with his friends  i am the best 

cryptographer  no one will ever decipher this ";

        chatbutton.onClick.AddListener(() => chat());               //chat button
        manualbutton.onClick.AddListener(() => manual());           //manual button
        shiftbutton.onClick.AddListener(() => shift_alphabet());
        right_button.onClick.AddListener(() => goForward());
        Destroy(Level_4, 3);
        Destroy(blackoutScreen, 3);


        int new_crypto = rnd.Next(1, 25);                           //create a random number

        for (int i = 0; i < plaintemp.Length; i++)
        {
            plaintemp[i] = " ";                                     //make plaintemp an array of all spaces
        }

        for (int i = 0; i < plain.Length; i++)
        {
            temp = plain[i];                                        //set temp equal to whatever is in plain[i]
            if (plain[i] >= 97 && plain[i] <= 122)                  //if plain is a letter
            {
                int temp = plain[i];                                // change whatever is in plain to an integer

                temp = (int)temp + new_crypto;                      //add the random number to it
                if (temp > 122)                                     //if it is larger than z
                {
                    temp = (temp - 122) + 96;                       //wrap it around back to a
                }
                temp = (char)temp;                                  // change it back into a character

                crypto[i] = (char)temp;                             //put it into the crypto array
                ciphertext1.text += crypto[i];                      //print it
            }
            else                                                    //if it isn't a letter
            {
                crypto[i] = plain[i];                               //dont change it and put it in the array
                ciphertext1.text += crypto[i];                      //print it
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        string input = inputfield.text;                             //get the input
        int length = crypto.Length;                                 //get the length of the crypto array
        string[] input_split = input.Split(' ');                    //split up the input
        string plaintempjoin = "";                                  //initialize plaintempjoin as a blank string
        int count;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (input_split.Length == 3)
            {
                if (input_split[1] == ">")
                {
                    output_text.text += ">>     " + input + "\n";
                    string one_char = input_split[2];                       //get the second part of the input
                    for (int i = 0; i < length; i++)
                    {
                        if (crypto[i].ToString() == input_split[0])
                        {
                            if (one_char[0] < 'a' || one_char[0] > 'z')
                            {
                                plaintempjoin += plaintemp[i];
                            }
                            else
                            {
                                plaintemp[i] = one_char[0].ToString();         //only print the first character if multiple character character are input
                                plaintempjoin += plaintemp[i];                 //create a string
                            }

                        }
                        else if (crypto[i] == '\n')
                        {
                            plaintemp[i] = "\n";                           //put a new line to the array
                            plaintempjoin += plaintemp[i];                 //create a string
                        }
                        else
                        {

                            plaintempjoin += plaintemp[i];                 //create a string
                        }

                    }

                    plaintext.text = plaintempjoin;                         // print it out


                    count = 0;
                    for (int i = 0; i < plaintempjoin.Length; i++)
                    {
                        if (plaintempjoin[i] == compare[i])
                        {
                            count = count + 1;
                            if (count == 227)
                            {
                                winner();
                            }

                        }

                    }
                    inputfield.text = "";
                }
                inputfield.ActivateInputField();
            }
        }
    }
    

    void chat()
    {
        chat_man.text = @"Charles: This is plaintext. You can easily see that the word is PIZZA. Now,"
                + " let’s turn this word into a cipher.\nAda: Using a cipher wheel is a great way to help encrypt and decrypt"
                + " messages. All you have to do for a caesar cipher is shift the alphabet. In this instance, the red letters are"
                + " the letters of the plain message and the green letters are the letters of the coded text.\n Ada: We have shifted the "
                + "alphabet 7 letters. If the coded message is made by shifting 7 letters, the only way to make the message readable"
                + " again is to shift it back 7 letters.\nCharles: After shifting the alphabet 7 places, the letter ‘A’ becomes" +
                 " ‘T’, ‘B’ becomes ‘U’, and so on. Now the encrypting begins. Just match the original message PIZZA to the letters" +
                 " of the inner wheel. ‘P’ becomes ‘I’, ‘I’ becomes ‘B’, ‘Z’ becomes ‘S’, and ‘A’ becomes ‘T’. Oh, man, all this talk" +
                 " of pizza is making me hungry.\nAda: It looks like Charles found a coded message that we need to decrypt using a Caesar Cipher. A good" +
                 " place to start with ciphers like these (when you don’t know how many places to shift the alphabet) is to remember" +
                 " common words in your language as well as common letters. Remember: Every word must have a vowel in it and only" +
                 " certain letters can be by themselves.";
    }

    void manual()
    {
        chat_man.text = "Syntax: \n\n cipherletter > plaintextletter";
    }

    void shift_alphabet()
    {

        string cipher_alpha = ciphertext_alphabet.text;
        int length = cipher_alpha.Length;
        string join = "";
        char[] duplicate = new char[length];

        for (int i = 0; i < length; i++)
        {
            if(cipher_alpha[i] == ' ')
            {
                join += cipher_alpha[i];
            }
            else
            {
                if(i < length -1)
                {
                    duplicate[i] = cipher_alpha[i+2];
                    join += duplicate[i];
                }
                else
                {
                    duplicate[i] = cipher_alpha[0];
                    join += duplicate[i];
                }
            }
        }
        ciphertext_alphabet.text = join;
    }
    void winner()
    {
        ciphertext1.text = "";
        plaintext.text = @"no one will ever be able to read tHis messAge

because it is enCrypted. i can say whatever i

want. alan thinKs he is so smart. he only hangs

out with his friEnds. i am the best 

cryptographer. no one will ever decipheR this.";

        chat_man.text = "Charles: Don't forget to write down the password!\n\nOnce you are done, be sure to hit the next button.";

        right_button.interactable = true;
        chatbutton.interactable = false;
        manualbutton.interactable = false;
    }

    void goForward()
    {
        SceneManager.LoadScene(("Level4MidScene"));
    }

}



