using UnityEngine;
using System.Collections;

// For now, it has only horizontal and vertical movements, and Shoot events.
public class KeyboardController : Controller
{
	public override void Update ()
	{
		base.Update ();

		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(shoot != null)
				shoot();
		}
		
		float displacement = Input.GetAxis("Horizontal");
		if(displacement != 0)
		{
			moveHorizontal(displacement);
		}

		displacement = Input.GetAxis("Vertical");
		if (displacement != 0) 
		{
			moveVertical(displacement);
		}
	}
}
