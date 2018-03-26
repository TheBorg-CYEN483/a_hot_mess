using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Level3Logo : MonoBehaviour {

	public Button rightButton;
	public int scene = 0;


	// Use this for initialization
	void Start () 
	{
	   rightButton.onClick.AddListener(() => goForward());
	}
	
	// Update is called once per frame
	void Update () 
	{
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
		 if (scene == 0)
        {
            rightButton.interactable = true;  
        }
		
    }
   


	

	void goForward()
    {
        if(scene < 1)
        {
            scene += 1;
        }
        if (scene == 1)
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
