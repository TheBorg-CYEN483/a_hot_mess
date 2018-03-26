using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Level4Badge : MonoBehaviour {

    public Button right_button;

    // Use this for initialization
    void Start () {
        right_button.onClick.AddListener(() => go());
	}
	
	// Update is called once per frame
	void go () {
        SceneManager.LoadScene(("Level4_outro"));
    }
}
