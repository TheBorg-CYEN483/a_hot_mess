using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Level4Intro : MonoBehaviour {

    public Text plaintext;
    public Text ciphertext;
    public Text cipheralpha;
    public Text plainalpha;
    public Text dialogue;
    public GameObject firsthooded;
    public GameObject secondhooded;
    public GameObject Linus;
    public Button leftButton;
    public Button rightButton;
    public Image wall, floor, legr, legl, keyboard, monitor, computer, disk, power,table, panel;
    public Image solitaire;
    public int scene = 0;
    public float time = 3;
        

	// Use this for initialization
	void Start () {
        rightButton.onClick.AddListener(() => goForward());
        leftButton.onClick.AddListener(() => goBackward());

    }
	
	// Update is called once per frame
	void Update () {
        string[] arraydialogue = { "Linus: Hey! How did you get in here? \nAda: I want to see what’s going on! Put me on Facetime!",
        "Ada: Wait, Isn’t that Alan’s little brother? \nCharles: Since when is he an evil genius? He’s like ten.",
        "Linus: You guys are always hogging him! He’s always in the basement hacking stuff with your stupid Super Secret"
        + " Hacker Society, and he never even hangs out with me. You can’t have him back! He’s encrypted in a file on my computer! " +
        "I bet you don’t even know what encryption means!", "Linus: Encryption is when you take something in regular words, or " +
        "plaintext, and turn it into something no one can read, known as a cipher. The only way to be able to read it again is to " +
        "turn it back into plaintext. There are a lot of ways to turn plaintext into a cipher.", "Ada: Oh? Which one is your favorite? " +
        "\nLinus : The Caesar Cipher, of course! It is the most well known cipher.", "Ada: Luckily for us, we know all about encryption! Let’s find him!",
        "Charles: This is plaintext. You can easily see that the word is PIZZA. Now,"
        + " let’s turn this word into a cipher.", "Ada: Using a cipher wheel is a great way to help encrypt and decrypt"
        + " messages. All you have to do for a caesar cipher is shift the alphabet. In this instance, the red letters are"
        + " the letters of the plain message and the blue letters are the letters of the coded text.", "Ada: We have shifted the "
        + "alphabet 7 letters. If the coded message is made by shifting 7 letters, the only way to make the message readable" 
        +" again is to shift it back 7 letters.", "Charles: After shifting the alphabet 7 places, the letter ‘A’ becomes" +
        " ‘H’, ‘B’ becomes ‘I’, and so on. Now the encrypting begins. Just match the original message PIZZA to the blue letters" +
        ". ‘P’ becomes ‘W’, ‘I’ becomes ‘P’, ‘Z’ becomes ‘G’, and ‘A’ becomes ‘H’. Oh, man, all this talk" +
        " of pizza is making me hungry.", "Ada: It looks like Charles found a coded message that we need to decrypt using a Caesar Cipher. A good" +
        " place to start with ciphers like these (when you don’t know how many places to shift the alphabet) is to remember" +
        " common words in the English language as well as common letters. Remember: Every word must have a vowel in it and only" +
        " certain letters can be by themselves."};

        if (scene < 11)
        {
            dialogue.text = arraydialogue[scene];
        }
        Debug.Log(scene);
        

        if (scene == 0)
        {
            Linus.SetActive(true);
            leftButton.interactable = false;
        }
        else if(scene == 5)
        {
            firsthooded.SetActive(false);
            secondhooded.SetActive(true);
        }
        else if(scene < 6)
        {
            leftButton.interactable = true;
        }

        if (scene == 6)
        {
            Linus.SetActive(false);
            leftButton.interactable = false;
            rightButton.interactable = true;
            panel.color = new Color(255, 255, 255, 255);
            wall.color = new Color(0, 0, 0, 0);
            floor.color = new Color(0, 0, 0, 0);
            legr.color = new Color(0, 0, 0, 0);
            legl.color = new Color(0, 0, 0, 0);
            keyboard.color = new Color(0, 0, 0, 0);
            monitor.color = new Color(0, 0, 0, 0);
            computer.color = new Color(0, 0, 0, 0);
            disk.color = new Color(0, 0, 0, 0);
            power.color = new Color(0, 0, 0, 0);
            table.color = new Color(0, 0, 0, 0);
            plaintext.color = new Color(255, 0, 0, 255);
            ciphertext.color = new Color(0, 255, 255, 0);
            cipheralpha.color = new Color(0, 255, 255, 0);
            plainalpha.color = new Color(255, 0, 0, 0);
            panel.color = new Color(0, 0, 0, 255);
            solitaire.color = new Color(255, 255, 255, 0);
        }
        else if (scene == 7)
        {
            cipheralpha.text = "a b c d e f g h i j k l m n o p q r s t u v w x y z";
            leftButton.interactable = true;
            rightButton.interactable = true;
            plaintext.color = new Color(255, 0, 0, 0);
            ciphertext.color = new Color(0, 255, 255, 0);
            cipheralpha.color = new Color(0, 255, 255, 255);
            plainalpha.color = new Color(255, 0, 0, 255);
        }
        else if (scene == 8)
        {
            cipheralpha.text = "h i j k l m n o p q r s t u v w x y z a b c d e f g";
            leftButton.interactable = true;
            rightButton.interactable = true;
            plaintext.color = new Color(255, 0, 0, 0);
            ciphertext.color = new Color(0, 255, 255, 0);
            cipheralpha.color = new Color(0, 255, 255, 255);
            plainalpha.color = new Color(255, 0, 0, 255);
        }
        else if (scene == 9)
        {
            //cipheralpha.text = "a b c d e f g h i j k l m n o p q r s t u v w x y z";
            leftButton.interactable = true;
            rightButton.interactable = true;
            plaintext.color = new Color(255, 0, 0, 255);
            ciphertext.color = new Color(0, 255, 255, 255);
        }
	}

    void goForward()
    {
        if (scene < 11)
        {
            scene += 1;
        }
        if(scene == 11)
        {
            SceneManager.LoadScene(("Level 4"));
        }
    }

    void goBackward()
    {
        if (scene > 0)
        {
            scene -= 1;
        }
    }

}
