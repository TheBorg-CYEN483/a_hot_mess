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
        transform.Translate(speed, 0, 0);
        curr_pos = transform.position;
        Debug.Log(curr_pos.x);
        if(curr_pos.x == 1800)
        {
            transform.Translate(init_pos.x-curr_pos.x, 0, 0);
        }
	}
}
