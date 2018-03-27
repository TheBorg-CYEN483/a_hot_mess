using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastHandler
{
	private Broadcast braodcastPrefab;
	private List<Broadcast> messages;
	private List<GameObject> nodes;
	private bool msgVisible;

	public BroadcastHandler(List<GameObject> a_nodes)
	{
		nodes = a_nodes;
		messages = new List<Broadcast> ();
		initBroadcasts ();
		msgVisible = false;
	}

	public void toggleBroadcastVis()
	{
		msgVisible = !msgVisible;
		if (msgVisible) {
			foreach (Broadcast msg in messages)
				msg.Show ();
		} else {
			foreach (Broadcast msg in messages)
				msg.Hide ();
		}
	}

	private void initBroadcasts()
	{
		// nodes[0] should be Level Router
		// for each client in the level
		for (int i = 1; i < nodes.Count - 1; i++)
		{
			messages.Add(Object.Instantiate (braodcastPrefab, nodes[i].transform));
			messages [i].Initialise (nodes [i], nodes [0]);
			messages [i].Hide ();
		}
	}
}
