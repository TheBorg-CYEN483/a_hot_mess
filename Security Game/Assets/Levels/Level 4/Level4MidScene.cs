using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level4MidScene : MonoBehaviour {
    public Text dialogue;
    public Button left_button;
    public Button right_button;
    int scene = 0;

	// Use this for initialization
	void Start () {
        right_button.onClick.AddListener(() => goForward());
        left_button.onClick.AddListener(() => goBackward());
    }
	
	// Update is called once per frame
	void Update () {
        string[] dialogue_box = {"Charles: Great! We got the password! We don't know what it goes to yet," +
                " but I think right now we just need to access their file system and FIND Alan. Ada, don't we" +
                " have a command for that?", "Ada: Shh, Charles. They know how to use the manual. Let them figure it out." };

        dialogue.text = dialogue_box[scene];
        
        if(scene == 0)
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
        if (scene == 2)
        {
            SceneManager.LoadScene(("Level4_2"));
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
