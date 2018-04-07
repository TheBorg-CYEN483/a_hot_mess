using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stairs3 : MonoBehaviour {

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
        yield return new WaitForSeconds(5.5f);
        SceneManager.LoadScene(("Level 3 intro"));
    }
}
