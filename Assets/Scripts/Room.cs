using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
	[SerializeField] private GameObject[] floorMarkers;

	public Vector2 GetPlayerTarget()
	{
		// TODO account for stuff in room
		// TODO calculate position better
		return (floorMarkers[0].transform.position + floorMarkers[1].transform.position) / 2;
	}
}
