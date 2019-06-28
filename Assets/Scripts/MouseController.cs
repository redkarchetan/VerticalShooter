using UnityEngine;
using System.Collections;

// This class is used to trap mouse events and send them to listeners.
public class MouseController : Controller 
{
	public float _MouseSensitivity = 1f;
	private Vector3 mPrevMousePosition;

	public override void Start ()
	{
		base.Start ();
		mPrevMousePosition = Input.mousePosition;
	}

	public override void Update ()
	{
		base.Update ();
		
		if(Input.GetMouseButtonDown(0))
		{
			if(shoot != null)
				shoot();
		}
		
		Vector2 displacement = Input.mousePosition - mPrevMousePosition;
		if(displacement.x != 0) 
		{
			moveHorizontal(displacement.x * _MouseSensitivity);
		}

		if (displacement.y != 0) 
		{
			moveVertical(displacement.y *_MouseSensitivity);
		}

		mPrevMousePosition = Input.mousePosition;
	}
}
