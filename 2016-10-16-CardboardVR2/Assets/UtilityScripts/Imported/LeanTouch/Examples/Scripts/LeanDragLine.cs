using UnityEngine;
using System.Collections.Generic;

namespace Lean.Touch
{
	// This script will draw a line between the start and current finger points
	public class LeanDragLine : MonoBehaviour
	{
		[Tooltip("The line prefab")]
		public LineRenderer Prefab;

		// This stores all the lines that are spawned while a finger is active
		private List<LineRenderer> lines = new List<LineRenderer>();
	
		protected virtual void OnEnable()
		{
			// Hook into the events we need
			LeanTouch.OnFingerSet += OnFingerSet;
			LeanTouch.OnFingerUp  += OnFingerUp;
		}

		protected virtual void OnDisable()
		{
			// Unhook the events
			LeanTouch.OnFingerSet -= OnFingerSet;
			LeanTouch.OnFingerUp  -= OnFingerUp;
		}

		private void OnFingerSet(LeanFinger finger)
		{
			// Make sure the prefab exists
			if (Prefab != null)
			{
				// Get the name of this finger, and find the line that has the same name
				var fingerName = GetFingerName(finger);
				var line       = lines.Find(l => l.name == fingerName);

				// If the line doesn't exist, create it, give it the same name as the finger, and add it to the lines list
				if (line == null)
				{
					line = Instantiate(Prefab);

					line.name = fingerName;

					lines.Add(line);
				}

				// Write the start and end positions to the line renderer
				line.SetVertexCount(2);
				
				line.SetPosition(0, finger.GetStartWorldPosition(1.0f));
				line.SetPosition(1, finger.GetWorldPosition(1.0f));
			}
		}

		private void OnFingerUp(LeanFinger finger)
		{
			// Get the name of this finger, and find the line that has the same name
			var fingerName = GetFingerName(finger);
			var line       = lines.Find(l => l.name == fingerName);

			// If the line exists, remove it from the lines list, and destroy it
			if (line != null)
			{
				lines.Remove(line);

				Destroy(line.gameObject);
			}
		}

		// This will generate a unique name based on the finger index
		private string GetFingerName(LeanFinger finger)
		{
			return finger.Index.ToString();
		}
	}
}