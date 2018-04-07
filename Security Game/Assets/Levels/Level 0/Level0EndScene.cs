using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level0EndScene : MonoBehaviour
{

    public Text text;
    public Button rightButton;
    public Button leftButton;
    public Image noise;
    public Image alan;
    int scene = 0;
    float i = 1f;
    bool fadeIn = true;

    // Use this for initialization
    void Start()
    {
        rightButton.onClick.AddListener(() => goForward());
        leftButton.onClick.AddListener(() => goBackward());
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(scene);
        if(scene == 2)
        {
            alan.color = new Color(255, 255, 255, 1f);
            noise.color = new Color(255, 255, 255, 0);
            i = 0f;
        }
        else if(scene == 3)
        {
            noise.transform.Rotate(0, 0, 40f);
            if (fadeIn)
            {
                noise.color = new Color(255, 255, 255, i);
                i += .01f;

                if (i >= 1f)
                {
                    fadeIn = false;
                }
            }
            else
            {
                if (i >= 0f)
                {
                    alan.color = new Color(255, 255, 255, i);
                    noise.color = new Color(255, 255, 255, i);
                    i -= .005f;
                }
            }
        }
        else if (scene == 4)
        {
            alan.color = new Color(255, 255, 255, 0);
            noise.color = new Color(255, 255, 255, 0);

        }
        text.text = text_array[scene];
    }

    string[] text_array = {"Ada: Thanks for setting up the firewall! You did way better than Alan would have!",
        "Alan: Thanks Ada. Hey, why is our network still acting funny?",
        "Charles: Someone got in anyway! They’re taking our files!",
        "Ada: Alan!",
        "Charles: They kidnapped him! New kid, you’re up! It looks like the connection is coming from a tower to the east of here. " +
            "You’re going to have to break in and rescue Alan!",
        "Ada: We’ll be here to help you along the way! Be sure to check the chat box for hints from us.",
        ""};

    void goForward()
    {
        Debug.Log("forward");
        if (scene < 6)
        {
            scene += 1;
        }
        if (scene == 6)
        {
            PlayerPrefs.SetString(PlayerPrefs.GetString("Username") + "." + PlayerPrefs.GetString("Password") + ".Level", "Tower");
            SceneManager.LoadScene("Tower");
        }
    }

    void goBackward()
    {
        Debug.Log("backward");
        if (scene > 0)
        {
            scene -= 1;
        }
    }
}

