using UnityEngine;

namespace Lean.Touch
{
	// This script will zoom the main camera based on finger gestures
	public class LeanSideCamera3D : MonoBehaviour
	{
		[Tooltip("The minimum field of view angle we want to zoom to")]
		public float FovMin = 10.0f;
		
		[Tooltip("The maximum field of view angle we want to zoom to")]
		public float FovMax = 60.0f;

		[Tooltip("The distance from the camera the world positions will be sampled from")]
		public float Distance = 10.0f;
		
		protected virtual void LateUpdate()
		{
			// Does the main camera exist?
			if (Camera.main != null)
			{
				// Get the world delta of all the fingers
				var worldDelta = LeanTouch.GetDeltaWorldPosition(Distance); // Distance doesn't matter with an orthographic camera
				
				// Subtract the delta to the position
				Camera.main.transform.position -= worldDelta;

				// Make sure the pinch scale is valid
				if (LeanTouch.PinchScale > 0.0f)
				{
					// Store the old FOV in a temp variable
					var fieldOfView = Camera.main.fieldOfView;

					// Scale the FOV based on the pinch scale
					fieldOfView /= LeanTouch.PinchScale;
					
					// Clamp the FOV to out min/max values
					fieldOfView = Mathf.Clamp(fieldOfView, FovMin, FovMax);

					// Set the new FOV
					Camera.main.fieldOfView = fieldOfView;
				}
			}
		}
	}
}