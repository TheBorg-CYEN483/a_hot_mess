using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level0IntroScript : MonoBehaviour {

    public Button rightButton;
    public Button leftButton;
    public Text IPs;
    public Text dialogue;
    public Text IP1;
    public Text IP2;
    public Text IP3;
    public Text IP4;
    public Text IP5;
    public Text IP6;
    public Text IP7;
    public Text IP8;
    public Text IP9;
    public Image firewall;
    public Text blacklistText;
    public Image blacklist;
    public Image blackbackground;

    int scene = 0;

    // Use this for initialization
    void Start () {
        rightButton.onClick.AddListener(() => goForward());
        leftButton.onClick.AddListener(() => goBackward());
    }
	
	// Update is called once per frame
	void Update () {
        dialogue.text = dialogue_array[scene];

        if (scene == 0)
        {
            IPs.text = "192.168.1.100\n192.168.1.101\n192.168.1.102\n192.168.1.103";
            IPs.color = new Color(0, 255, 0, 255);
            leftButton.interactable = false;
            rightButton.interactable = true;
            IP1.color = new Color(0, 0, 0, 255);
            IP2.color = new Color(0, 0, 0, 255);
            IP3.color = new Color(0, 0, 0, 255);
            IP4.color = new Color(0, 0, 0, 255);
            IP5.color = new Color(0, 0, 0, 255);
            IP6.color = new Color(0, 0, 0, 255);
            IP7.color = new Color(0, 0, 0, 255);
            IP8.color = new Color(0, 0, 0, 255);
            IP9.color = new Color(0, 0, 0, 255);
            firewall.color = new Color(0, 0, 0, 255);
            blacklist.color = new Color(0, 0, 0, 0);
            blacklistText.color = new Color(0, 0, 0, 0);
        }

        if (scene == 1)
        {
            IPs.text = "\n\n10.29.14.67";
            IPs.color = new Color(255, 0, 0, 255);
            leftButton.interactable = true;
            rightButton.interactable = true;
            IP1.color = new Color(0, 0, 0, 255);
            IP2.color = new Color(0, 0, 0, 255);
            IP3.color = new Color(0, 0, 0, 255);
            IP4.color = new Color(0, 0, 0, 255);
            IP5.color = new Color(0, 0, 0, 255);
            IP6.color = new Color(0, 0, 0, 255);
            IP7.color = new Color(0, 0, 0, 255);
            IP8.color = new Color(0, 0, 0, 255);
            IP9.color = new Color(0, 0, 0, 255);
            firewall.color = new Color(0, 0, 0, 255);
            blacklist.color = new Color(0, 0, 0, 0);
            blacklistText.color = new Color(0, 0, 0, 0);
        }

        if (scene == 2)
        {
            IPs.text = "";
            IP1.color = new Color(255, 0, 0, 255);
            IP2.color = new Color(255, 0, 0, 255);
            IP3.color = new Color(255, 0, 0, 255);
            IP4.color = new Color(255, 0, 0, 255);
            IP5.color = new Color(0, 255, 0, 255);
            IP6.color = new Color(0, 255, 0, 255);
            IP7.color = new Color(0, 255, 0, 255);
            IP8.color = new Color(0, 255, 0, 255);
            IP9.color = new Color(255, 0, 0, 255);
            firewall.color = new Color(255, 255, 0, 255);
            blacklist.color = new Color(0, 0, 0, 0);
            blacklistText.color = new Color(0, 0, 0, 0);
            leftButton.interactable = true;
            rightButton.interactable = true;
            blackbackground.color = new Color(0, 0, 0, 0);
        }

        if(scene == 3)
        {
            leftButton.interactable = true;
            rightButton.interactable = true;
            blacklist.color = new Color(255, 0, 0, 255);
            blacklistText.color = new Color(255, 0, 0, 255);
            blackbackground.color = new Color(0, 0, 0, 255);
        }
    }
   

    string[] dialogue_array = {"Alan: Okay, see these IP addresses here? Each one is a computer in this basement." +
            " 192.168.1.100 is mine, 192.168.1.101 is Ada’s computer, and 192.168.1.102 is Charles’ computer. Yours " +
            "is 192.168.1.103.", "But look at this IP address!\nIt’s not any of our computers. It has to be someone outside!" +
            " Someone’s trying to hack in!", "Ada: This yellow line here is our firewall. See how IP addresses are passing through" +
            " it? Only the green ones should be able to get through. You need to tell the firewall to block the red ones. ",
            "You do that by adding the red IP addresses to a “blacklist”, which is just a list of which IPs to block.", ""};

    void goForward()
    {
        if(scene < 4)
        {
            scene += 1;
        }
        if (scene == 4)
        {
            SceneManager.LoadScene(("Level 0"));
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
