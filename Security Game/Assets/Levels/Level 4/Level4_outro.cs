using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level4_outro : MonoBehaviour {

    public Button left_button;
    public Button right_button;
    public Text dialogue;
    public int scene = 0;

    // Use this for initialization
    void Start () {
        right_button.onClick.AddListener(() => goForward());
        left_button.onClick.AddListener(() => goBackward());
    }
	
	// Update is called once per frame
	void Update ()
    {
        string[] arraydialogue = {"Alan: Linus! Do you see why I don’t let you play with my computer?\nLinus: Yeah. Don’t tell mom.",
        "Ada: Actually, he did a pretty good job of setting up all these challenges. I think we could use him in the group. \nCharles: Agreed!",
        "Alan: Linus, welcome to the Super Secret Hacking Society!"};

        dialogue.text = arraydialogue[scene];

        if (scene == 0)
        {
            left_button.interactable = false;
            right_button.interactable = true;
        }
        else
        {
            left_button.interactable = true;
            right_button.interactable = true;
        }
    }
    
    void goForward()
    {
        if (scene < 2)
        {
            scene += 1;
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
