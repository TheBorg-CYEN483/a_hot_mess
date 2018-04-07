using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpriteScript : MonoBehaviour
{
    public GameObject player;
    public float smoothTime = 0.3F;
    public float velocity = 0.0F;
    Vector3 targetPosition = Vector3.zero;

    // Use this for initialization
    void Start ()
    {
        player = gameObject.transform.Find("Player").gameObject;

    }

    // Update is called once per frame
    void Update ()
    {

        targetPosition = (new Vector3(398, 608, 0));
        float PlayerNewPosx = Mathf.SmoothDamp(player.transform.position.x, targetPosition.x, ref velocity, smoothTime);
        float PlayerNewPosy = Mathf.SmoothDamp(player.transform.position.x, targetPosition.y, ref velocity, smoothTime);
        player.transform.position = new Vector3(PlayerNewPosx, PlayerNewPosy, transform.position.z);

    }
}
