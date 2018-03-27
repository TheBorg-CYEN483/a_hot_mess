using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BadgeScript : MonoBehaviour {

    public Button rightButton;

    // Use this for initialization
    void Start () {
        rightButton.onClick.AddListener(() => goForward());
    }
	
	// Update is called once per frame
	void Update () {

    }

    void goForward()
    {
        SceneManager.LoadScene(("Level0EndScene"));
        PlayerPrefs.SetString("Level", "Level0EndScene");
    }
}
