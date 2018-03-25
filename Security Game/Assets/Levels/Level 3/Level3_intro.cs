using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level3_intro : MonoBehaviour 
{
    public GameObject Gabriel;
    public GameObject Morgan;
    public GameObject Pat;
 	public Button rightButton;
    public Button leftButton;
	public Text dialogue;
	public int scene = 0;


	// Use this for initialization
	void Start () 
	{
	   rightButton.onClick.AddListener(() => goForward());
       leftButton.onClick.AddListener(() => goBackward());
	}
	
	// Update is called once per frame
	void Update () 
	{
		dialogue.text = dialogue_array[scene];
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
		Gabriel = gameObject.transform.GetChild(3).gameObject;//gameObject refers to the object running the script
    	Morgan = gameObject.transform.GetChild(4).gameObject;
        Pat = gameObject.transform.GetChild(2).gameObject;
		Gabriel.SetActive(false);
		Morgan.SetActive(false);
		Pat.SetActive(false);
		 if (scene == 0)
        {
          
            leftButton.interactable = false;
            rightButton.interactable = true;
           
        }
		  if (scene == 1)
        {
            leftButton.interactable = true;
            rightButton.interactable = true;
           
        }

        if (scene == 2)
        {
            Gabriel.SetActive(true);
			Morgan.SetActive(true);
			Pat.SetActive(true);
            leftButton.interactable = true;
            rightButton.interactable = true;
        }

        if(scene == 3)
        {
			Gabriel.SetActive(false);
			Morgan.SetActive(false);
			Pat.SetActive(false);
            leftButton.interactable = true;
            rightButton.interactable = true;
        }
    }
    string[] dialogue_array = 
			{"Ada: Hey, it looks like your computer is acting funny. Did you screw it up?\n" +
            "Charles: I bet his computer was hacked. Maybe you should look for malware.", 
			"Malware is any program that do not want, need, or ask for.\n", 
            "Ada: We’re going to send some software to you. \nIt’s called MalChecker, and it will scan your files for malware.\n" +
            "Charles: This is what malware looks like.\n",
            "Charles: Malchecker is sort of like using a flashlight in a dark room to look for monsters. In this case, the monsters are malware." +
			"So the MalChecker will show us where the malware is hiding in your folders.", ""};
   


	

	void goForward()
    {
        if(scene < 4)
        {
            scene += 1;
        }
        if (scene == 4)
        {
            SceneManager.LoadScene(("Level 3"));
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
