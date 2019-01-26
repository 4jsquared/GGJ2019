using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float money;
	public Rigidbody2D root;

	private Queue<GameObject> route;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void GoTo(Room room)
	{
		// TODO transition
		Vector2 target = room.GetPlayerTarget();

		root.MovePosition(target);
	}
}
