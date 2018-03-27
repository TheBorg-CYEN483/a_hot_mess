﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Broadcast : MonoBehaviour
{
	private GameObject node1, node2;
	private Vector2 source, target, velocity;
	private int sequence;
	private Color color;

	public void Initialise (GameObject a_node1, GameObject a_node2)
	{
		node1 = a_node1;
		node2 = a_node2;
		source = new Vector2(node1.transform.position.x, node1.transform.position.y);
		target = new Vector2(node2.transform.position.x, node2.transform.position.y);
		setTarget (target, source);
		sequence = 0;
		color = node1.GetComponent<Image>().color;

		setPosition(source + velocity);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (get2DPosition() == target) 
		{
			sequence++;
			if (sequence >= 6)
				Destroy (this.gameObject);
//			color = (sequence % 2 == 0) ? node2.GetComponent<Image> ().color : node1.GetComponent<Image> ().color;
			setTarget(source, target);
		}
		setPosition (get2DPosition() + velocity);
	}

	//njust move it way offscreen
	public void Hide()
	{
		setPosition (source + 100000 * velocity);
	}

	public void Show()
	{
		setPosition (source + (1 + 2*Random.value)*velocity/4);
	}

	private void setPosition(Vector2 pos)
	{
		this.transform.position = new Vector3 (pos.x, pos.y, 0);
	}

	private Vector2 get2DPosition()
	{
		return new Vector2 (this.transform.position.x, this.transform.position.y);
	}

	private void setTarget(Vector2 a_target, Vector2 a_src)
	{
		target = a_target;
		source = a_src;
		velocity = (target - source)/90;
	}
}
