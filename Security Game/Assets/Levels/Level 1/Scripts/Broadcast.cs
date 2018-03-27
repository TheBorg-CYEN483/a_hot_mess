using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Broadcast : MonoBehaviour
{
	private Vector2 source, target, velocity;
	private int sequence;
	private Color color_id;

	public Broadcast (GameObject node1, GameObject node2)
	{
		source = new Vector2(node1.transform.position.x, node1.transform.position.y);
		target = new Vector2(node2.transform.position.x, node2.transform.position.y);
		velocity = (target - source) / 90;
		sequence = 0;
		color_id = node1.GetComponent<Image>().color;

		setPosition(source + velocity);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		setPosition (get2DPosition() + velocity);
	}

	public void setPosition(Vector2 pos)
	{
		this.transform.position = new Vector3 (pos.x, pos.y, 0);
	}

	private Vector2 get2DPosition()
	{
		return new Vector2 (this.transform.position.x, this.transform.position.y);
	}
}
