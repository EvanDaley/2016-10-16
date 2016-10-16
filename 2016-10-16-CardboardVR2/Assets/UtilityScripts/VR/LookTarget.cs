
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class LookTarget : MonoBehaviour, IGvrGazeResponder {
	//private Vector3 startingPosition;

	public Color hitColor = Color.green;
	public Color nonhitColor = Color.red;

	void Start() {
		//startingPosition = transform.localPosition;
		SetGazedAt(false);
	}

	void LateUpdate() {
		GvrViewer.Instance.UpdateState();
		if (GvrViewer.Instance.BackButtonPressed) {
			Application.Quit();
		}
	}

	public void SetGazedAt(bool gazedAt) {
		GetComponent<Renderer>().material.color = gazedAt ? hitColor : nonhitColor;
	}
		
	public virtual void FireEvent() 
	{

	}

	#region IGvrGazeResponder implementation

	/// Called when the user is looking on a GameObject with this script,
	/// as long as it is set to an appropriate layer (see GvrGaze).
	public void OnGazeEnter() {
		SetGazedAt(true);
	}

	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	public void OnGazeExit() {
		SetGazedAt(false);
	}

	/// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
	public void OnGazeTrigger() {
		FireEvent();
	}

	#endregion
}
