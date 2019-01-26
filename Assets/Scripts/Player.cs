using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float money;

	[SerializeField] private Rigidbody2D root;
	[SerializeField] private Room targetRoom;
	[SerializeField] private float moveSpeed;
	
	private Queue<GameObject> route;
	private Vector2 nextTarget;
	private World world;

	// Use this for initialization
	void Start ()
	{
		route = new Queue<GameObject>();

		// Make sure we start in the correct location
		nextTarget = targetRoom.GetPlayerTarget();
		root.MovePosition(nextTarget);
	}
	
	// Update is called once per frame
	void Update()
	{
		float remainingDistance = Time.deltaTime * moveSpeed;
		Vector2 endPosition = root.transform.position;
		bool reachedTarget = false;

		// While we have distance left to move, move
		while (remainingDistance > 0)
		{
			float distanceToTarget = Vector2.Distance(nextTarget, endPosition);
			float fractionTravelled = 1;
			Vector2 target = nextTarget;

			if (distanceToTarget <= remainingDistance)
			{
				remainingDistance -= distanceToTarget;

				// Still got further to go - check if we can go further
				if (route.Count > 0)
				{
					// Check for any more nav points.
					GameObject nextMarker = route.Dequeue();
					nextTarget = nextMarker.transform.position;
				}
				else if (!reachedTarget)
				{
					// Move within the room, but we've reached our target
					nextTarget = targetRoom.GetPlayerTarget();
					reachedTarget = true;
				}
				else
				{
					break;
				}
			}
			else
			{
				fractionTravelled = remainingDistance / distanceToTarget;
				remainingDistance = 0;
			}

			endPosition += (target - endPosition) * Mathf.Clamp01(fractionTravelled);
		}

		root.MovePosition(endPosition);
	}

	public void Initialise(World inWorld)
	{
		world = inWorld;
	}

	public void GoTo(Room room)
	{
		// Generate a route from our current target (probably where we are) to our target
		IEnumerable<GameObject> nextRoute = world.GetRoute(targetRoom, room);
		foreach (GameObject navPoint in nextRoute)
			route.Enqueue(navPoint);

		// Set our new target
		targetRoom = room;
	}
}
