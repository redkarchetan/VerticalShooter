using UnityEngine;
using System.Collections;

// This is a generic input controller, which will capture inputs and raise events.
// Any class can listen to these events and implement corresponding behaviour.
// We can extend this controller to add different input devices like joystick, keyboard, mouse, touch, etc. 
// One class can be extended per input device, please see implementation of KeyBoardController and MouseController.

public class Controller : MonoBehaviour 
{
	public delegate void Shoot();
	public delegate void MoveHorizontal(float value);
	public delegate void MoveVertical(float value);

	public static Shoot shoot;
	public static MoveHorizontal moveHorizontal;
	public static MoveVertical moveVertical;

	public virtual void Start () 
	{
	
	}
	
	public virtual void Update()
	{

	}
}
