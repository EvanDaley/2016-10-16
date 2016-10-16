using UnityEngine;

namespace Lean.Touch
{
	// This script will zoom the main camera based on finger gestures
	public class LeanSideCamera2D : MonoBehaviour
	{
		[Tooltip("The minimum field of view angle we want to zoom to")]
		public float Minimum = 10.0f;
		
		[Tooltip("The maximum field of view angle we want to zoom to")]
		public float Maximum = 60.0f;
		
		protected virtual void LateUpdate()
		{
			// Does the main camera exist?
			if (Camera.main != null)
			{
				// Get the world delta of all the fingers
				var worldDelta = LeanTouch.GetDeltaWorldPosition(1.0f); // Distance doesn't matter with an orthographic camera
				
				// Subtract the delta to the position
				Camera.main.transform.position -= worldDelta;

				// Make sure the pinch scale is valid
				if (LeanTouch.PinchScale > 0.0f)
				{
					// Store the old size in a temp variable
					var orthographicSize = Camera.main.orthographicSize;

					// Scale the size based on the pinch scale
					orthographicSize /= LeanTouch.PinchScale;
					
					// Clamp the size to out min/max values
					orthographicSize = Mathf.Clamp(orthographicSize, Minimum, Maximum);

					// Set the new size
					Camera.main.orthographicSize = orthographicSize;
				}
			}
		}
	}
}