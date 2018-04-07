using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Continue : MonoBehaviour {
    public InputField username;
    public InputField password;

    public void ClickLogin()
    {
        if (username.text != "" && password.text != "")
        {
            PlayerPrefs.SetString("Username", username.text);
            PlayerPrefs.SetString("Password", password.text);
            PlayerPrefs.SetString(username.text + "." + password.text + ".Level", "Level0Scene");
            SceneManager.LoadScene("Level0Scene");
        }
    }
}
