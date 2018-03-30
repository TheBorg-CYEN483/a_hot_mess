using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    public Button leftButton;
    public Text dialogue;
    private int page = 0;
    // private Dictionary<int, Action> pageMethods;
    private List<string> openingDialogue = new List<string>() {
        "Charles: That was impressive. Remind me to stay on your good side, wouldn't want you near my router.",
        "Ada: Yes, yes, good for you, don't do it again. Now go to the next floor.",
        };

    public void ClickNext()
    {
        if (page < (openingDialogue.Count - 1))
        {
            page++;
        }
        else
        {
            // SceneManager.LoadScene("Level 2_Intro");
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
