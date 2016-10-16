using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to transform the current GameObject
	public class LeanTransform : MonoBehaviour
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
				Rotate(LeanTouch.TwistDegrees);
			}

			if (AllowScale == true)
			{
				Scale(LeanTouch.PinchScale);
			}
		}

		private void Translate(Vector2 screenPositionDelta)
		{
			// Screen position of the transform
			var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
			
			// Add the deltaPosition
			screenPosition += (Vector3)screenPositionDelta;
			
			// Convert back to world space
			transform.position = Camera.main.ScreenToWorldPoint(screenPosition);
		}

		private void Rotate(float angleDelta)
		{
			transform.rotation *= Quaternion.Euler(0.0f, 0.0f, angleDelta);
		}

		private void Scale(float scale)
		{
			// Make sure the scale is valid
			if (scale > 0.0f)
			{
				// Grow the local scale by scale
				transform.localScale *= scale;
			}
		}
	}
}