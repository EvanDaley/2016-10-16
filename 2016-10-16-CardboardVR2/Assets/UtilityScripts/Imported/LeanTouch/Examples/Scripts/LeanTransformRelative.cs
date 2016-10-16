using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to transform the current GameObject
	public class LeanTransformRelative : MonoBehaviour
	{
		public bool AllowTranslate = true;

		public bool AllowRotate = true;

		public bool AllowScale = true;

		protected virtual void Update()
		{
			if (AllowTranslate == true)
			{
				Translate(LeanTouch.DragDelta);
			}

			if (AllowRotate == true)
			{
				RotateRelative(LeanTouch.TwistDegrees, LeanTouch.CenterOfFingers);
			}

			if (AllowScale == true)
			{
				ScaleRelative(LeanTouch.PinchScale, LeanTouch.CenterOfFingers);
			}
		}

		public void Translate(Vector2 screenPositionDelta)
		{
			// Screen position of the transform
			var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
			
			// Add the deltaPosition
			screenPosition += (Vector3)screenPositionDelta;
			
			// Convert back to world space
			transform.position = Camera.main.ScreenToWorldPoint(screenPosition);
		}

		public void RotateRelative(float angleDelta, Vector2 referencePoint)
		{
			// World position of the reference point
			var worldReferencePoint = Camera.main.ScreenToWorldPoint(referencePoint);
		
			// Rotate the transform around the world reference point
			transform.RotateAround(worldReferencePoint, Camera.main.transform.forward, angleDelta);
		}

		public void ScaleRelative(float scale, Vector2 referencePoint)
		{
			// Make sure the scale is valid
			if (scale > 0.0f)
			{
				// Screen position of the transform
				var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
			
				// Push the screen position away from the reference point based on the scale
				screenPosition.x = referencePoint.x + (screenPosition.x - referencePoint.x) * scale;
				screenPosition.y = referencePoint.y + (screenPosition.y - referencePoint.y) * scale;
			
				// Convert back to world space
				transform.position = Camera.main.ScreenToWorldPoint(screenPosition);
			
				// Grow the local scale by scale
				transform.localScale *= scale;
			}
		}
	}
}