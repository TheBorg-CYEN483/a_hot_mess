using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public GameObject inspector;
    public InputField username;
    public InputField password;
    public Text output;
    private string user = "admin";
    private string pass = "password";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            inspector.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            inspector.SetActive(false);
        }
    }

    public void ClickLogin()
    {
        if (username.text == user && password.text == pass)
        {
            Debug.Log("Logged in...");
            // SceneManager.LoadScene("Level 0");
        }
        // else if (Login credentials match existing user) {
        //		SceneManager.LoadScene(PlayerPrefs.GetString("Level"));
        // }
        else
        {
            output.text = "Incorrect username or password";
        }
    }
}
