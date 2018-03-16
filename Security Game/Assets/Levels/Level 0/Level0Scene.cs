using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level0Scene : MonoBehaviour {

    public Text text;
    public Button rightButton;
    public Button leftButton;
    int scene = 0;

    // Use this for initialization
    void Start () {
        rightButton.onClick.AddListener(() => goForward());
        leftButton.onClick.AddListener(() => goBackward());
    }
	
	// Update is called once per frame
	void Update () {
        text.text = text_array[scene];
        
    }

    string[] text_array = {"Ada: Looks like you’ve passed our initiation! Welcome to the Super Secret Hacking Society!", "Charles: Hi! " +
                "Welcome to the club. We usually hang out down here and work on our hacking skills. " +
                "This is our network, which just means a bunch of computers that are connected together.", "Alan: I’m working on learning " +
                "about IP addresses today. An IP address is a number we use to label each device that’s on a network.", "My computer is" +
                " connected to the network we have here in the basement, so it’s labeled with 192.168.1.100.", "Ada: Remember to set up that firewall," +
                " Alan. That way, we can block out any computers we don’t want on the network. Think of it like a filter.", "Alan: Will do." +
                " Actually, it looks like we’re getting some pretty weird IP addresses coming through the network right now. New kid, could you come" +
                " look at this?", ""};

    void goForward()
    {
        if (scene < 6)
        {
            scene += 1;
        }
        if(scene == 6)
        {
            SceneManager.LoadScene(("Level0Intro"));
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

