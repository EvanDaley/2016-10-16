using UnityEngine;
using System.Collections;

public class ActivateOnDetect : MonoBehaviour {

	public GameObject ObjectToEnable;
	
	void DetectObject()
	{
		if(ObjectToEnable != null)
			ObjectToEnable.SetActive (true);
	}
}
