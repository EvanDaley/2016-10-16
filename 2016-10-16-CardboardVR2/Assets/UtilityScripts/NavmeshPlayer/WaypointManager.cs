using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour {

	public List<Waypoint> allWaypoints;
	public static WaypointManager Instance;

	void Awake () 
	{
		Instance = this;
	}
	
	public void RegisterWaypoint(Waypoint waypoint)
	{
		allWaypoints.Add (waypoint);
	}

	public Waypoint FindNextWaypointForward(int curPosition)
	{
		int closestNumberAbove = int.MaxValue;
		Waypoint closestWaypointAbove = null;

		for (int i = 0; i < allWaypoints.Count; i++)
		{
			Waypoint possibleNextWaypoint = allWaypoints [i];
			if (curPosition < possibleNextWaypoint.value)
			{
				if (possibleNextWaypoint.value < closestNumberAbove)
				{
					closestNumberAbove = possibleNextWaypoint.value;
					closestWaypointAbove = possibleNextWaypoint;
				}
			}
		}

		return closestWaypointAbove;
	}

	public Waypoint FindNextWaypointBackward(int curPosition)
	{
		int closestNumberBelow = int.MinValue;
		Waypoint closestWaypointBelow = null;

		for (int i = 0; i < allWaypoints.Count; i++)
		{
			Waypoint possiblePrevWaypoint = allWaypoints [i];
			if (curPosition > possiblePrevWaypoint.value)
			{
				if (possiblePrevWaypoint.value > closestNumberBelow)
				{
					closestNumberBelow = possiblePrevWaypoint.value;
					closestWaypointBelow = possiblePrevWaypoint;
				}
			}
		}

		return closestWaypointBelow;
	}
}