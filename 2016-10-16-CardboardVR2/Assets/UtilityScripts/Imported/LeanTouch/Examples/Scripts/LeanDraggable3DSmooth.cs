using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to drag this GameObject using any finger, as long it has a collider
	[RequireComponent(typeof(Collider))]
	public class LeanDraggable3DSmooth : MonoBehaviour
	{
		[Tooltip("This stores the layers we want the raycast to hit (make sure this GameObject's layer is included!)")]
		public LayerMask LayerMask = UnityEngine.Physics.DefaultRaycastLayers;
		
		[Tooltip("How quickly smoothly this GameObject moves toward the target position")]
		public float Sharpness = 10.0f;

		// This stores the finger that's currently dragging this GameObject
		private LeanFinger draggingFinger;

		// The position this object will smoothly move to
		private Vector3 targetPosition;
		
		protected virtual void OnEnable()
		{
			// Make the target position match the current position at the start
			targetPosition = transform.position;

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
					
					// Convert the screen position into world coordinates and add the change to the target position
					targetPosition += Camera.main.ScreenToWorldPoint(screenPosition) - transform.position;
				}
			}

			// The framerate independent damping factor
			var factor = Mathf.Exp(- Sharpness * Time.deltaTime);
			
			// Dampen the current position toward the target
			transform.position = Vector3.Lerp(targetPosition, transform.position, factor);
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