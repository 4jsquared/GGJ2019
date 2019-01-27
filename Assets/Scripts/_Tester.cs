using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Tester : MonoBehaviour
{
	[SerializeField] private Storyteller storyteller;

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Alpha0))
			storyteller.player.GoTo(storyteller.world.rooms[0], () => { });
		if (Input.GetKeyDown(KeyCode.Alpha1))
			storyteller.player.GoTo(storyteller.world.rooms[1], () => { });
		if (Input.GetKeyDown(KeyCode.Alpha2))
			storyteller.player.GoTo(storyteller.world.rooms[2], () => { });
		if (Input.GetKeyDown(KeyCode.Alpha3))
			storyteller.player.GoTo(storyteller.world.rooms[3], () => { });
		if (Input.GetKeyDown(KeyCode.Alpha4))
			storyteller.player.GoTo(storyteller.world.rooms[4], () => { });
		if (Input.GetKeyDown(KeyCode.Alpha5))
			storyteller.player.GoTo(storyteller.world.rooms[5], () => { });
	}
}
