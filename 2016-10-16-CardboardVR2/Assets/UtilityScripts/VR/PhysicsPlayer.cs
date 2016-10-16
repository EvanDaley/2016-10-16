using UnityEngine;
using System.Collections;

public class PhysicsPlayer : MonoBehaviour {

	public float moveSpeed = 2f;
	public float moveDuration = 1f;

	private float moveCooldown = 0f;
	private bool moving = false;
	private Vector3 moveDirection;
	private Rigidbody rbody;

	void Start () 
	{
		rbody = GetComponent<Rigidbody> ();
	}
	
	void Update () 
	{
		if (Time.time < moveDuration)
		{
			Move (moveDirection);
		}
	}

	void SetMove(Vector3 direction)
	{
		moving = true;
		moveDirection = direction;
		moveCooldown = Time.time + moveDuration;
	}

	void Move(Vector3 direction)
	{
		rbody.AddForce (direction * moveSpeed * Time.deltaTime);
	}


}
