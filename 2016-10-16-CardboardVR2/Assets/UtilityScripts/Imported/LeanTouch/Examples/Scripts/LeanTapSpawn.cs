using UnityEngine;

namespace Lean.Touch
{
	// This script will spawn a prefab when you tap the screen
	public class LeanTapSpawn : MonoBehaviour
	{
		[Tooltip("The prefab that gets spawned upon clicking")]
		public GameObject Prefab;

		[Tooltip("The amount of seconds it takes for the prefab to get automatically destroyed after being spawned")]
		public float AutoDestroyDelay = 2.0f;
		
		protected virtual void OnEnable()
		{
			// Hook into the events we need
			LeanTouch.OnFingerTap += OnFingerTap;
		}
		
		protected virtual void OnDisable()
		{
			// Unhook the events
			LeanTouch.OnFingerTap -= OnFingerTap;
		}
		
		private void OnFingerTap(LeanFinger finger)
		{
			// Does the prefab exist?
			if (Prefab != null)
			{
				// Make sure the finger isn't over any GUI elements
				if (finger.IsOverGui == false)
				{
					// Clone the prefab, and place it where the finger was tapped
					var position = finger.GetWorldPosition(50.0f);
					var rotation = Quaternion.identity;
					var clone    = (GameObject)Instantiate(Prefab, position, rotation);
					
					// Make sure the prefab gets destroyed after some time
					Destroy(clone, AutoDestroyDelay);
				}
			}
		}
	}
}