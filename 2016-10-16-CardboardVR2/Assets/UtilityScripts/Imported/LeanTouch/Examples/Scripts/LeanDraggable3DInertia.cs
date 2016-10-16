using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to drag this GameObject using any finger and throw it, as long it has a collider and rigidbody
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(Rigidbody))]
	public class LeanDraggable3DInertia : MonoBehaviour
	{
		[Tooltip("This stores the layers we want the raycast to hit (make sure this GameObject's layer is included!)")]
		public LayerMask LayerMask = UnityEngine.Physics.DefaultRaycastLayers;
		
		// This stores the finger that's currently dragging this GameObject
		private LeanFinger draggingFinger;

		// Cached rigidbody attached to this gameObject
		private Rigidbody body;
		
		protected virtual void OnEnable()
		{
			// Get attached Rigidbody?
			if (body == null)
			{
				body = GetComponent<Rigidbody>();
			}

			// Hook into the events we need
			LeanTouch.OnFingerDown += OnFingerDown;
			LeanTouch.OnFingerUp   += OnFingerUp;
		}
		
		protected virtual void OnDisable()
		{
			// Unhook the events
			LeanTouch.OnFingerDown -= OnFingerDown;
			LeanTouch.OnFingerUp   -= OnFingerUp;
		}
		
		protected virtual void LateUpdate()
		{
			// If there is an active finger, move this GameObject based on it
			if (draggingFinger != null)
			{
				// Does the main camera exist?
				if (Camera.main != null)
				{
					// Convert this GameObject's world position into screen coordinates and store it in a temp variable
					var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
					
					// Modify screen position by the finger's delta screen position
					screenPosition += (Vector3)draggingFinger.DeltaScreenPosition;
					
					// Convert the screen position into world coordinates and update this GameObject's world position with it
					transform.position = Camera.main.ScreenToWorldPoint(screenPosition);
					
					// Reset velocity
					body.velocity = Vector3.zero;
				}
			}
		}
		
		private void OnFingerDown(LeanFinger finger)
		{
			// Raycast information
			var ray = finger.GetRay();
			var hit = default(RaycastHit);
			
			// Raycast below the current finger
			if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask) == true)
			{
				// Did the raycast hit this transform, or a child of it?
				if (IsChildOf(hit.transform) == true)
				{
					// Set the current finger to this one
					draggingFinger = finger;
				}
			}
		}
		
		private void OnFingerUp(LeanFinger finger)
		{
			// Was the current finger lifted from the screen?
			if (finger == draggingFinger)
			{
				// Unset the current finger
				draggingFinger = null;
				
				// Convert this GameObject's world position into screen coordinates and store it in a temp variable
				var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
				
				// Modify screen position by the finger's delta screen position over the past 0.1 seconds
				screenPosition += (Vector3)finger.GetSnapshotDelta(0.1f);
				
				// Convert the screen position into world coordinates and subtract it by the old position to find the world delta over the past 0.1 seconds
				var worldDelta = Camera.main.ScreenToWorldPoint(screenPosition) - transform.position;
				
				// Set the velocity and divide it by 0.1, because velocity is applied over 1 second, and our delta is currently only for 0.1 second
				body.velocity = worldDelta / 0.1f;
			}
		}

		// Does transform match other? Or other.parent? etc
		private bool IsChildOf(Transform target)
		{
			if (target != null)
			{
				if (target == transform)
				{
					return true;
				}

				return IsChildOf(target.parent);
			}

			return false;
		}
	}
}