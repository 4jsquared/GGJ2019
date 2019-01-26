using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class World : MonoBehaviour
{
	public float time;

	public Room[] rooms;

	private class NavPoint
	{
		public GameObject marker;
		public List<NavPoint> connectedNodes;
	}

	Dictionary<GameObject, NavPoint> navPoints;

	private void Start()
	{
		navPoints = new Dictionary<GameObject, NavPoint>();

		// Fill in the navPoints for later use
		foreach (Room room in rooms)
		{
			NavPoint prevNavPoint = null;

			// Get (or create) all of the room nav points
			foreach (GameObject marker in room.FloorMarkers)
			{
				NavPoint curNavPoint;
				if (!navPoints.TryGetValue(marker, out curNavPoint))
				{
					// New nav point
					curNavPoint = new NavPoint();
					curNavPoint.connectedNodes = new List<NavPoint>();
					curNavPoint.marker = marker;

					// Add it to the list
					navPoints[marker] = curNavPoint;
				}

				// Connect up the current and previous nav points
				if (prevNavPoint != null)
				{
					curNavPoint.connectedNodes.Add(prevNavPoint);
					prevNavPoint.connectedNodes.Add(curNavPoint);
				}

				prevNavPoint = curNavPoint;
			}
		}
	}

	public IEnumerable<GameObject> GetRoute(Room from, Room to)
	{
		// Spider until we get where we want
		Dictionary<GameObject, GameObject> web = new Dictionary<GameObject, GameObject>();
		GameObject endMarker = null;

		Queue<GameObject> toProcess = new Queue<GameObject>();

		// The node in the from room has no prev node - it's the start
		foreach (GameObject fromMarker in from.FloorMarkers)
		{
			web[fromMarker] = null;
			toProcess.Enqueue(fromMarker);
		}

		// For each node to process
		while (toProcess.Count > 0)
		{
			// Grab the node
			GameObject marker = toProcess.Dequeue();

			// Check what it's connected to.
			IEnumerable<GameObject> connectedMarkers = navPoints[marker].connectedNodes.Select(x => x.marker);
			foreach (GameObject connectedMarker in connectedMarkers)
			{
				// If this is already added, we already have a (shorter) path.
				if (!web.ContainsKey(connectedMarker))
				{
					// Not connected, add it as connected from our current node.
					web[connectedMarker] = marker;
					toProcess.Enqueue(connectedMarker);
				}

				if (to.FloorMarkers.Contains(connectedMarker))
				{
					endMarker = connectedMarker;
					break;
				}
			}

			if (endMarker != null)
				break;
		}

		// We *should* have an end node at this point, create the list by walking back through the web from the end point.
		List<GameObject> route = new List<GameObject>();
		while (endMarker != null)
		{
			route.Add(endMarker);
			endMarker = web[endMarker];
		}

		// It's in the reverse order, so return in the correct order.
		return route;
	}
}
