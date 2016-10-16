using UnityEngine;

namespace Lean.Touch
{
	// This script will hook into event LeanTouch event, and spam the console with the information
	public class LeanTouchEvents : MonoBehaviour
	{
		protected virtual void OnEnable()
		{
			// Hook into the events we need
			LeanTouch.OnFingerDown     += OnFingerDown;
			LeanTouch.OnFingerSet      += OnFingerSet;
			LeanTouch.OnFingerUp       += OnFingerUp;
			LeanTouch.OnFingerDrag     += OnFingerDrag;
			LeanTouch.OnFingerTap      += OnFingerTap;
			LeanTouch.OnFingerSwipe    += OnFingerSwipe;
			LeanTouch.OnFingerHeldDown += OnFingerHeldDown;
			LeanTouch.OnFingerHeldSet  += OnFingerHeld;
			LeanTouch.OnFingerHeldUp   += OnFingerHeldUp;
			LeanTouch.OnMultiTap       += OnMultiTap;
			LeanTouch.OnDrag           += OnDrag;
			LeanTouch.OnSoloDrag       += OnSoloDrag;
			LeanTouch.OnMultiDrag      += OnMultiDrag;
			LeanTouch.OnPinch          += OnPinch;
			LeanTouch.OnTwistDegrees   += OnTwistDegrees;
			LeanTouch.OnTwistRadians   += OnTwistRadians;
		}
		
		protected virtual void OnDisable()
		{
			// Unhook the events
			LeanTouch.OnFingerDown     -= OnFingerDown;
			LeanTouch.OnFingerSet      -= OnFingerSet;
			LeanTouch.OnFingerUp       -= OnFingerUp;
			LeanTouch.OnFingerDrag     -= OnFingerDrag;
			LeanTouch.OnFingerTap      -= OnFingerTap;
			LeanTouch.OnFingerSwipe    -= OnFingerSwipe;
			LeanTouch.OnFingerHeldDown -= OnFingerHeldDown;
			LeanTouch.OnFingerHeldSet  -= OnFingerHeld;
			LeanTouch.OnFingerHeldUp   -= OnFingerHeldUp;
			LeanTouch.OnMultiTap       -= OnMultiTap;
			LeanTouch.OnDrag           -= OnDrag;
			LeanTouch.OnSoloDrag       -= OnSoloDrag;
			LeanTouch.OnMultiDrag      -= OnMultiDrag;
			LeanTouch.OnPinch          -= OnPinch;
			LeanTouch.OnTwistDegrees   -= OnTwistDegrees;
			LeanTouch.OnTwistRadians   -= OnTwistRadians;
		}
		
		public void OnFingerDown(LeanFinger finger)
		{
			Debug.Log("Finger " + finger.Index + " began touching the screen");
		}
		
		public void OnFingerSet(LeanFinger finger)
		{
			Debug.Log("Finger " + finger.Index + " is still touching the screen");
		}
		
		public void OnFingerUp(LeanFinger finger)
		{
			Debug.Log("Finger " + finger.Index + " finished touching the screen");
		}
		
		public void OnFingerDrag(LeanFinger finger)
		{
			Debug.Log("Finger " + finger.Index + " moved " + finger.DeltaScreenPosition + " pixels across the screen");
		}
		
		public void OnFingerTap(LeanFinger finger)
		{
			Debug.Log("Finger " + finger.Index + " tapped the screen");
		}
		
		public void OnFingerSwipe(LeanFinger finger)
		{
			Debug.Log("Finger " + finger.Index + " swiped the screen");
		}
		
		public void OnFingerHeldDown(LeanFinger finger)
		{
			Debug.Log("Finger " + finger.Index + " began touching the screen for a long time");
		}
		
		public void OnFingerHeld(LeanFinger finger)
		{
			Debug.Log("Finger " + finger.Index + " is still touching the screen for a long time");
		}
		
		public void OnFingerHeldUp(LeanFinger finger)
		{
			Debug.Log("Finger " + finger.Index + " stopped touching the screen for a long time");
		}
		
		public void OnMultiTap(int fingerCount)
		{
			Debug.Log("The screen was just tapped by " + fingerCount + " finger(s)");
		}
		
		public void OnDrag(Vector2 pixels)
		{
			Debug.Log("One or many fingers moved " + pixels + " across the screen");
		}
		
		public void OnSoloDrag(Vector2 pixels)
		{
			Debug.Log("One finger moved " + pixels + " across the screen");
		}
		
		public void OnMultiDrag(Vector2 pixels)
		{
			Debug.Log("Many fingers moved " + pixels + " across the screen");
		}
		
		public void OnPinch(float scale)
		{
			Debug.Log("Many fingers pinched " + scale + " percent");
		}
		
		public void OnTwistDegrees(float angle)
		{
			Debug.Log("Many fingers twisted " + angle + " degrees");
		}
		
		public void OnTwistRadians(float angle)
		{
			Debug.Log("Many fingers twisted " + angle + " radians");
		}
	}
}