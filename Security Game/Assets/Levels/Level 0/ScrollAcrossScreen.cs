using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollAcrossScreen : MonoBehaviour {

    public float speed = 2f;
    private Vector3 init_pos;
    private Vector3 curr_pos;
 

    // Use this for initialization
    void Start () {
        init_pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        curr_pos = transform.position;
        //stop IP at firewall if it has been blocked
        if (Level0.helper(gameObject.name))
        {
            if (curr_pos.x < 199 || curr_pos.x > 201)
            {
                transform.Translate(speed, 0, 0);
                if (curr_pos.x >= 1200)
                {
                    transform.position = new Vector3(-1000, curr_pos.y, curr_pos.z);
                }
            }
            else
            {
                transform.Translate(0, 0, 0);
            }
        }
        else if (curr_pos.x >= 1200)
        {
            transform.position = new Vector3(-1000, curr_pos.y, curr_pos.z);
        }
        else
        {
            transform.Translate(speed, 0, 0);
        }
        
    }
}
