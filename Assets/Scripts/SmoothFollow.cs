using UnityEngine;
using System.Collections;

// This is a camera script, which follows the target object.
public class SmoothFollow : MonoBehaviour 
{
	public float _Distance;
	public GameObject _Target;

	private Transform mTransform;
	private Transform mTargetTransform;

	void Start()
	{
		mTransform = transform;
		if(_Target != null)
		{
			mTargetTransform = _Target.transform;
		}
	}

	void LateUpdate()
	{
		mTransform.position = new Vector3(mTransform.position.x, mTransform.position.y, mTargetTransform.position.z + _Distance);
	}
}
