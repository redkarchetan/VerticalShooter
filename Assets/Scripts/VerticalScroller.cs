using UnityEngine;
using System.Collections;

// This calss will help control scroll though one axis.
public class VerticalScroller : MonoBehaviour 
{
	public Vector3 _DefaultDisplacement = new Vector3(0, 0, 5f);
	
	private Transform mTransform;

	void Start()
	{
		mTransform = transform;
	}

	void Update () 
	{
		if (!GameManager.pInstance.pIsGamePaused) 
		{
			mTransform.position = mTransform.position + _DefaultDisplacement * Time.deltaTime;
		}
	}
}
