using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

	[Tooltip("An int representing this waypoints position. A larger number means it is found later in the game")]
	public int value = 0;

	void Start () 
	{
		WaypointManager.Instance.RegisterWaypoint (this);
	}
}
