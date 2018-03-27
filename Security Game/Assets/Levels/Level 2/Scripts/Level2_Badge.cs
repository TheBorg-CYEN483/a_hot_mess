using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2_Badge : MonoBehaviour
{
    public void Click()
    {
        PlayerPrefs.SetString("Level", "Level 2_End");
        SceneManager.LoadScene("Level 2_End");
    }
}
