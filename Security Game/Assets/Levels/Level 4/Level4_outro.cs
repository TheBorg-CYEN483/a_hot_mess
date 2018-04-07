using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level4_outro : MonoBehaviour {

    public Button left_button;
    public Button right_button;
    public Text dialogue;
    public GameObject turntoalan;
    public GameObject turntolinus;
    public GameObject turntocamera;
    public Image noise;
    public Image alan;
    int scene = 0;
    float i = 0f;
    bool fadeIn = true;

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
            turntolinus.SetActive(false);
            
            turntoalan.GetComponent<SpriteRenderer>().color = new Color (255, 255, 255, 255);
            noise.transform.Rotate(0, 0, 40f);
            if (fadeIn)
            {
                noise.color = new Color(255, 255, 255, i);
                    alan.color = new Color(255, 255, 255, i);
                i += .005f;

                if (i >= 1f)
                {
                    fadeIn = false;
                }
            }
            else
            {
                if (i >= 0f)
                {
                    noise.color = new Color(255, 255, 255, i);
                    i -= .01f;
                }
            }
            left_button.interactable = false;
            right_button.interactable = true;
        }
        else if(scene == 1)
        {
            turntolinus.SetActive(true);
            turntoalan.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            turntocamera.SetActive(false);
            alan.color = new Color(255, 255, 255, 255);
            noise.color = new Color(255, 255, 255, 0);
            left_button.interactable = true;
            right_button.interactable = true;
        }
        else if(scene ==2)
        {
            turntolinus.SetActive(false);
            
            turntocamera.SetActive(true);
            left_button.interactable = true;
            right_button.interactable = true;
        }
        else
        {
            fadeIn = true;
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
        else if(scene == 2)
        {
            PlayerPrefs.SetString(PlayerPrefs.GetString("Username") + "." + PlayerPrefs.GetString("Password") + ".Level", "You Win");
            SceneManager.LoadScene("You Win");
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
