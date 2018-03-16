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
    int scene = 0;

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
        text.text = text_array[scene];
    }

    string[] text_array = {"Ada: Thanks for setting up the firewall! You did way better than Alan would have!",
        "Alan: Thanks Ada.Hey, why is our network still acting funny?",
        "Charles: Someone got in anyway! They’re taking our files!",
        "Ada: Alan!",
        "Charles: They kidnapped him! New kid, you’re up! It looks like the connection is coming from a tower to the east of here. " +
            "You’re going to have to break in and rescue Alan!",
        "Ada: We’ll be here to help you along the way! Be sure to check the chat box for hints from us.",
        ""};

    void goForward()
    {
        Debug.Log("forward");
        if (scene < 7)
        {
            scene += 1;
        }
        if (scene == 7)
        {
            SceneManager.LoadScene(("Tower"));
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

