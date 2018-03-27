using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastHandler
{
	private List<Broadcast> messages;
	private List<GameObject> nodes;
	private bool msgVisible;

	public BroadcastHandler(List<GameObject> a_nodes)
	{
		nodes = a_nodes;
		msgVisible = false;
	}

	public void toggleBroadcastVis()
	{
		foreach (Broadcast msg in messages)
		{
			msg.gameObject.SetActive(!msg.gameObject.activeSelf);
		}
	}
}
