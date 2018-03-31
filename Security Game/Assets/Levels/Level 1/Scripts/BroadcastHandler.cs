using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastHandler
{
	private List<GameObject> broadcasts;
	private List<GameObject> nodes;
	private bool msgVisible;

	public BroadcastHandler(List<GameObject> a_nodes, List<GameObject> a_broadcasts)
	{
		nodes = a_nodes;
		broadcasts = a_broadcasts;
		initBroadcasts ();
		msgVisible = false;
	}

	public void toggleBroadcastVis()
	{
		msgVisible = !msgVisible;
		if (msgVisible) {
			foreach (GameObject bcast in broadcasts)
				bcast.SetActive (true);
		} else {
			foreach (GameObject bcast in broadcasts)
				bcast.SetActive (false);
		}
	}

	private void initBroadcasts()
	{
		if (nodes.Count != 0) 
		{
			// nodes[0] should be Level Router
			// for each client in the level
			for (int i = 0; i <= nodes.Count - 2; i++) 
			{
				broadcasts [i].AddComponent<Broadcast> ();
				broadcasts [i].GetComponent<Broadcast> ().Initialise (nodes [i+1], nodes [0]);
				broadcasts [i].SetActive (false);
			}
		}
	}
}
