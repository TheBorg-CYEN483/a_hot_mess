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
            SceneManager.LoadScene("NewUser");
        }
        else if (PlayerPrefs.GetString(username.text + "." + password.text + ".Level") != "") {
            PlayerPrefs.SetString("Username", username.text);
            PlayerPrefs.SetString("Password", password.text);
            SceneManager.LoadScene(PlayerPrefs.GetString(username.text + "." + password.text + ".Level"));
        }
        else
        {
            output.text = "Incorrect username or password";
        }
    }
}
