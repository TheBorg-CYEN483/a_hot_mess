using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level3Logo : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(waiting());
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator waiting()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(("Level 3"));
    }
}
