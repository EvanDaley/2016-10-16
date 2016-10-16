using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to drag this current GameObject
	[RequireComponent(typeof(Collider))]
	public class LeanDraggable3D : MonoBehaviour
	{
		[Tooltip("This stores the layers we want the raycast to hit (make sure this GameObject's layer is included!)")]
		public LayerMask LayerMask = UnityEngine.Physics.DefaultRaycastLayers;
		
		// This stores the finger that's currently dragging this GameObject
		private LeanFinger draggingFinger;
		
		protected virtual void OnEnable()
		{
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