using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    public Button leftButton;
    public Text dialogue;
    private int page = 0;
    // private Dictionary<int, Action> pageMethods;
    private List<string> openingDialogue = new List<string>() {
        "Charles: More locked doors. I guess we should have seen that coming. But it looks like this door is constantly receiving a signal to stay locked.",
        "Ada: The signal is going through a router, or access point, in the room. Problem is, there are several other signals going through the access point going to different places. I can't narrow down what IP the signal could be coming from.",
        "Charles: I don't usually suggest this, but you will have to try a Man-in-the-Middle attack to find, intercept, and modify the signal that is telling the door to stay locked.",
        "Ada: We should look for another option first. [Name] could get in a lot of trouble for doing that.",
        "Charles: We don’t have that kind of time. Alan needs us. To do this, you need to find the IP address sending the signal that is keeping the door locked.",
        "Then, redirect the signal to your computer instead. When you receive the signal, modify it to unlock the door and send it to the access point.",
        "Ada: Your understanding of this frightens me, but if you think [Name] can do it then I won’t argue. This time.",
        };

    public void ClickNext()
    {
        if (page < (openingDialogue.Count - 1))
        {
            page++;
        }
        else
        {
            SceneManager.LoadScene("Level 2_Intro");
        }
        leftButton.interactable = true;
    }

    public void ClickPrev()
    {
        if (page > 0)
        {
            page--;
        }
        else
        {
            leftButton.interactable = false;
        }
    }

    void Update()
    {
        dialogue.text = openingDialogue[page];
    }
}
