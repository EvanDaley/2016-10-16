using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public PhysicsPlayer physicsPlayer;
	public GameObject cursor3d;

	public static MenuController Instance;

	void Awake()
	{
		Instance = this;
	}

	public void SwapToMenu(SubMenuType menuType)
	{
		if (menuType == SubMenuType.movePlayer)
		{
			print ("Switch to move");
			ResetCursorRotation ();

		}

		else if (menuType == SubMenuType.options)
		{
			print ("Switch to options");
			ResetCursorRotation ();

		}

		else if (menuType == SubMenuType.rotateCursor)
		{
			print ("Switch to rotate");
			ResetCursorRotation ();

		}

		else if (menuType == SubMenuType.scaleCursor)
		{
			print ("Switch to scale");
			ResetCursorRotation ();

		}

		else if (menuType == SubMenuType.translateCursor)
		{
			print ("Switch to translate");
			ResetCursorRotation ();
		}
	}

	public void ResetCursorRotation()
	{
		cursor3d.transform.rotation = Camera.main.transform.rotation;
		cursor3d.transform.localScale = Vector3.one;
	}

	public void Option1()
	{

	}

	public void Option2()
	{

	}

	public void Option3()
	{

	}

	public void Option4()
	{

	}


}
