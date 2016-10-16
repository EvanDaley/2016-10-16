using UnityEngine;
using System.Collections;

public class NavmeshPlayer : MonoBehaviour {

	public NavMeshAgent agent;
	public Vector3 target;
	public movementStates curMovementState;

	void Start()
	{
		agent.updateRotation = false;
	}

	public void UpdatePath(Vector3 target)
	{
		if (curMovementState != movementStates.locked)
		{
			agent.SetDestination (target);
			this.target = target;
		}
	}

	public void UpdatePath()
	{
		if (curMovementState != movementStates.locked)
		{
			agent.SetDestination (target);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 10)
		{
//			Waypoint waypoint = other.GetComponent<Waypoint> ();
//			if (waypoint != null)
//			{
//				if(movementStatus == 1 && previousWaypoint < waypoint.value)
//					previousWaypoint = waypoint.value;
//
//				if(movementStatus == 2 && previousWaypoint > waypoint.value)
//					previousWaypoint = waypoint.value;
//			}
			
			UpdatePath (target);
		}
	}

	void Update()
	{
		// look toward position that we are shooting at
		//FaceTarget (Vector3.zero);
	}
}

public enum movementStates
{
	forward,
	idle,
	reverse,
	auto,
	locked
}

