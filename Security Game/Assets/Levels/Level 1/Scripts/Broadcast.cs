using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Broadcast : MonoBehaviour
{
	private GameObject node1, node2;
	private Vector2 source, target, velocity;
	private int sequence;

	public void Initialise (GameObject a_node1, GameObject a_node2)
	{
		node1 = a_node1;
		node2 = a_node2;
		source = new Vector2(node1.transform.position.x, node1.transform.position.y);
		target = new Vector2(node2.transform.position.x, node2.transform.position.y);

		setTarget (target, source);
		sequence = 0;
		setColor (node1.GetComponent<Image>().color);
		setPosition (source + velocity*10*(1 + 2*Random.value )); // spawn randomly within middle half of bcast path
	}

	// Use this for initialization
	void Start () {	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log(this.gameObject.name + " @ " + get2DPosition().ToString("F4") + " aiming for " + target.ToString("F4"));
		if (Vector2.Distance (get2DPosition (), target) < 0.03)
		{
//			Debug.Log (this.gameObject.name + " hit " + target.ToString("F4") + ": _" + sequence);
			sequence++;

			setColor((sequence % 2 == 1) ? node2.GetComponent<Image> ().color : node1.GetComponent<Image> ().color);
			setTarget(source, target);
			this.gameObject.transform.Rotate (new Vector3 (0, 0, 180));
			if (sequence >= 6) 
			{
				this.gameObject.SetActive (false);
				StaticCoroutine.StartCoroutine (restartBroadcast ());
			}
		}
		setPosition (get2DPosition() + velocity);
	}


	private void setPosition(Vector2 pos)
	{
		this.transform.position = new Vector3 (pos.x, pos.y, 0);
	}

	private Vector2 get2DPosition()
	{
		return new Vector2 (this.transform.position.x, this.transform.position.y);
	}

	private void setColor(Color c)
	{
		this.gameObject.GetComponent<SpriteRenderer> ().color = c;
	}

	private void setTarget(Vector2 a_target, Vector2 a_src)
	{
		target = a_target;
		source = a_src;
		velocity = (target - source).normalized / 40;
//		Debug.Log (this.gameObject.name + " moving at " + velocity.ToString("F4"));
	}

	IEnumerator restartBroadcast()
	{
		yield return new WaitForSeconds (2 + 3 * Random.value);
		this.gameObject.SetActive (true);
		sequence = 0;
	}
}
